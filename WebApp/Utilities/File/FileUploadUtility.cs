using Microsoft.AspNetCore.Hosting;
using WebApp.ViewModels;

namespace WebApp.Utilities.File
{
    public class FileUploadUtility
    {
        private IFormFile formFile = null!;
        private string uniqueFileName = null!;
        private readonly IWebHostEnvironment webHostEnvironment;
        private string? destinationFolderName;

        public string DestinationFolderName
        {
            get
            {
                if (!string.IsNullOrEmpty(destinationFolderName))
                {
                    return destinationFolderName;
                }
                return string.Empty;
            }
            set { destinationFolderName = value; }
        }

        public FileUploadUtility(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string UploadFile(IFormFile formFile)
        {
            this.formFile = formFile;
            CreateUniqueFileName();
            CopyFileToDestinationFolder();
            return uniqueFileName;
        }

        private void CopyFileToDestinationFolder()
        {
            string absolutePhotoFileName = CreateAbsoluteFilePath();
            using FileStream fileStream = new(absolutePhotoFileName, FileMode.Create);
            formFile?.CopyTo(fileStream);
            fileStream.Dispose();
        }

        private string CreateAbsoluteFilePath()
        {
            string webRootPath = webHostEnvironment.WebRootPath;
            string absolutePhotoFileName;
            if (!string.IsNullOrEmpty(destinationFolderName))
            {
                absolutePhotoFileName = Path.Combine(webRootPath, destinationFolderName, uniqueFileName);
            }
            else
            {
                absolutePhotoFileName = Path.Combine(webRootPath, uniqueFileName);
            }
            return absolutePhotoFileName;
        }

        private void CreateUniqueFileName()
        {
            string fileName = $"{Guid.NewGuid()}_{formFile?.FileName}";
            this.uniqueFileName = fileName;
        }

    }
}
