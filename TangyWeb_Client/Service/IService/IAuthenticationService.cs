using Tangy_Models.DTO;

namespace TangyWeb_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequestDTO);
        Task<SignInResponseDTO> LoginUser(SignInRequestDTO signInRequestDTO);
        Task LogoutUser();
    }
}
