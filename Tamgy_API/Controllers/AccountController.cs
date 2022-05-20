using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tamgy_API.Helpers;
using Tangy_Common;
using Tangy_DataAccess;
using Tangy_Models.DTO;

namespace Tamgy_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly APISettings _apiSettings;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<APISettings> options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _apiSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
        {

            if (signUpRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser
            {
                Name = signUpRequestDTO.Name,
                Email = signUpRequestDTO.Email,
                UserName = signUpRequestDTO.Email,
                PhoneNumber = signUpRequestDTO.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO
                {
                    Errors = result.Errors.Select(x => x.Description),
                    IsRegistrationSuccessful = false

                });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, StaticData.Role_Customer);
            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO
                {
                    IsRegistrationSuccessful = false,
                    Errors = roleResult.Errors.Select(x => x.Description)
                });
            }

            return StatusCode(201);

        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {

            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.UserName, signInRequestDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new SignInResponseDTO
                    {
                        ErrorMessage = "Invalid Creds",
                        IsAuthSuccessful = false
                    });
                }

                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaimsAsync(user);

                var tokenOptions = new JwtSecurityToken(issuer:_apiSettings.ValidIssuer,
                    audience:_apiSettings.ValidAudience,
                    claims: claims,
                    expires:DateTime.Now.AddDays(30),
                    signingCredentials:signingCredentials);
                    
                var token=new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new SignInResponseDTO
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    User = new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new SignInResponseDTO
                {
                    ErrorMessage = "Invalid Creds",
                    IsAuthSuccessful = false
                });
            }
            return StatusCode(201);

        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser applicationUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,applicationUser.Email),
                new Claim(ClaimTypes.Email,applicationUser.Email),
                new Claim("Id",applicationUser.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(applicationUser.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
