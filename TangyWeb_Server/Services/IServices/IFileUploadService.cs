using Microsoft.AspNetCore.Components.Forms;

namespace TangyWeb_Server.Services.IServices
{
    public interface IFileUploadService
    {
        Task<string> FileUpload(IBrowserFile file);
        bool FileDelete(string filePath);
    }
}
