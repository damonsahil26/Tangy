using Microsoft.AspNetCore.Components;
using Tangy_Models.DTO;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages
{
    public partial class Register
    {

        public SignUpRequestDTO signUpRequest { get; set; } = new();
        public bool IsProcessing { get; set; }
        public bool IsRegistrationFailed { get; set; }
        public IEnumerable<string> Errors { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }

        public async Task RegisterUser()
        {
            IsProcessing = true;
            var result = await _authService.RegisterUser(signUpRequest);
            if (result.IsRegistrationSuccessful)
            {
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                Errors = result.Errors;
                IsRegistrationFailed = true;
            }
            IsProcessing = false;
        }
    }
}
