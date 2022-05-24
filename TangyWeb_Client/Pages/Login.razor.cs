using Microsoft.AspNetCore.Components;
using System.Web;
using Tangy_Models.DTO;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages
{
    public partial class Login
    {
        public SignInRequestDTO signInRequest { get; set; } = new();
        public bool IsProcessing { get; set; }
        public bool IsAuthFailed { get; set; }
        public string Error { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        public IAuthenticationService _authService { get; set; }

        public string ReturnURL { get; set; }

        public async Task LoginUser()
        {
            IsProcessing = true;
            var result = await _authService.LoginUser(signInRequest);
            if (result.IsAuthSuccessful)
            {
                var absoluteUri = new Uri(_navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnURL = queryParam["returnUrl"];
                if (string.IsNullOrWhiteSpace(ReturnURL))
                {
                    _navigationManager.NavigateTo("/");
                }
                else
                {
                    _navigationManager.NavigateTo("/" + ReturnURL);
                }
            }
            else
            {
                Error = result.ErrorMessage;
                IsAuthFailed = true;
            }
            IsProcessing = false;
        }
    }
}
