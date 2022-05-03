using Microsoft.AspNetCore.Components.Forms;
using TangyWeb_Server.Services.IServices;

namespace TangyWeb_Server.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool FileDelete(string filePath)
        {
            if (!filePath.Contains("default.png"))
            {
                if (File.Exists(_webHostEnvironment.WebRootPath + "/" + filePath))
                {
                    File.Delete(_webHostEnvironment.WebRootPath + "/" + filePath);
                    return true;
                }
            }
            return false;
        }
        public async Task<string> FileUpload(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\images\\product";

            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }

            var filePath = Path.Combine(folderDirectory, fileName);
            await using FileStream fileStream = new FileStream(filePath, FileMode.Create);


            await file.OpenReadStream().CopyToAsync(fileStream);
            var fullPath = $"images/product/{fileName}";
            return fullPath;
        }
    }
}
