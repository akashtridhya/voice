using voice.models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace voice.files.implementations
{
    public class UploadManager : IUploadManager
    {
        #region Constructor and Object Declaration

        private BaseUrlConfigs BaseUrlConfig { get; set; }

        public UploadManager(IOptions<BaseUrlConfigs> BaseUrlConfigOption)
        {
            BaseUrlConfig = BaseUrlConfigOption.Value;
        }

        #endregion Constructor and Object Declaration

        public void Delete(string name, string type)
        {
            using var store_file = new StoreToLocalFolder(BaseUrlConfig);
            store_file.Delete(name, type);
        }

        public void Delete(string filePath)
        {
            using var store_file = new StoreToLocalFolder(BaseUrlConfig);
            store_file.Delete(filePath);
        }

        public void DeleteDirectory(string folderPath)
        {
            using var store_file = new StoreToLocalFolder(BaseUrlConfig);
            store_file.DeleteDirectory(folderPath);
        }

        public async Task<string> Store(IFormFile file, string name, string type)
        {
            using var store_file = new StoreToLocalFolder(BaseUrlConfig);
            return await store_file.Upload(file, name, type);
        }

        public void Move(string sourceFilePath, string destinationFilePath)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!File.Exists(sourceFilePath)) return;

            File.Move(sourceFilePath, destinationFilePath);
        }

        public void MoveDirectory(string sourceFilePath, string destinationFilePath)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!Directory.Exists(sourceFilePath)) return;

            Directory.Move(sourceFilePath, destinationFilePath);
        }

        #region House Keeping

        public void Dispose()
        {
            Disponse(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Disponse(bool disposing)
        {
            if (disposing)
            {
            }
        }

        #endregion House Keeping
    }
}