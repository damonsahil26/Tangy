using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace TangyWeb_Client.Pages
{
    public partial class RedirectToLogin
    {
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> _authState { get; set; }

        public bool NotAuthorized { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await _authState;
            if (authState?.User?.Identity == null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    _navigationManager.NavigateTo("login");
                }
                else
                {
                    _navigationManager.NavigateTo($"login?returnUrl={returnUrl}");
                }
            }
            else
            {
                NotAuthorized = true;
            }
        }
    }
}
