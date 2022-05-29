using Microsoft.AspNetCore.Components;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Pages
{
    public partial class Logout
    {
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public IAuthenticationService _authenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await _authenticationService.LogoutUser();
            _navigationManager.NavigateTo("/");
        }

    }
}
