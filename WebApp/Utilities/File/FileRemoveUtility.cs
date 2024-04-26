using Microsoft.AspNetCore.Hosting;

namespace WebApp.Utilities.File
{
    public class FileRemoveUtility
    {
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

        public FileRemoveUtility(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public void RemoveFileFromDestinationFolder(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string absolutePhotoFileName = CreateAbsoluteFilePath(fileName);
                System.IO.File.Delete(absolutePhotoFileName);
            }
        }

        private string CreateAbsoluteFilePath(string fileName)
        {
            string webRootPath = webHostEnvironment.WebRootPath;
            string absolutePhotoFileName;
            if (!string.IsNullOrEmpty(destinationFolderName))
            {
                absolutePhotoFileName = Path.Combine(webRootPath, destinationFolderName, fileName);
            }
            else
            {
                absolutePhotoFileName = Path.Combine(webRootPath, fileName);
            }
            return absolutePhotoFileName;
        }

    }
}
