using Microsoft.JSInterop;

namespace TangyWeb_Server.Helpers
{
    public static class IJSRuntimeExtensions
    {
        public static async ValueTask ShowToastr(this IJSRuntime JSRuntime, string type, string message)
        {
            await JSRuntime.InvokeVoidAsync("showToastr", type, message);
        }
    }
}
