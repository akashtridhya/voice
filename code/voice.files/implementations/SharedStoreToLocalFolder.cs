using voice.models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SimpleImpersonation;
using System;
using System.IO;
using System.Threading.Tasks;

namespace voice.files.implementations
{
    public class SharedStoreToLocalFolder : IDisposable
    {
        private string storage_path = string.Empty;
        private string file_name = "", full_path = "";
        private SharedFolderCredentialsConfigs SharedFolderCredentialsConfigs { get; set; }

        public SharedStoreToLocalFolder(BaseUrlConfigs baseUrl,SharedFolderCredentialsConfigs SharedFolderCredentialsConfigs)
        {
            storage_path = baseUrl.ImageLocalPath;
            this.SharedFolderCredentialsConfigs = SharedFolderCredentialsConfigs;
        }

        public async Task<string> Upload(IFormFile file, string name, string folder)
        {
            if (!Directory.Exists($"{storage_path}//{folder}"))
                Directory.CreateDirectory($"{storage_path}//{folder}");

            file_name = $"{name}{Path.GetExtension(file.FileName)}";
            full_path = $"{storage_path}/{folder}/{file_name}";

            var credentials = new UserCredentials(
                 SharedFolderCredentialsConfigs.Domain,
                 SharedFolderCredentialsConfigs.Username,
                 SharedFolderCredentialsConfigs.Password);

            var result = Impersonation.RunAsUser(credentials, LogonType.Network, async () =>
            {
                using var fileStream = new FileStream(full_path, FileMode.Create);
                await file.CopyToAsync(fileStream);
            });

            return file_name;
        }

        public async Task<string> Export(string fileName, string content, string folder = null, string folderFullPath = null)
        {
            if (!string.IsNullOrEmpty(folder))
            {
                if (!Directory.Exists($"{storage_path}//{folder}"))
                    Directory.CreateDirectory($"{storage_path}//{folder}");

                var path = $"{storage_path}\\{folder}\\{fileName}";

                var credentials = new UserCredentials(
                    SharedFolderCredentialsConfigs.Domain,
                    SharedFolderCredentialsConfigs.Username,
                    SharedFolderCredentialsConfigs.Password);

                var result = Impersonation.RunAsUser(credentials, LogonType.Network, async () =>
                {
                    await File.WriteAllTextAsync(path, content);
                });

                return fileName;
            }

            if (!string.IsNullOrEmpty(folderFullPath))
            {
                if (!Directory.Exists(folderFullPath))
                    Directory.CreateDirectory(folderFullPath);

                var path = $"{folderFullPath}\\{fileName}";

                var credentials = new UserCredentials(
                  SharedFolderCredentialsConfigs.Domain,
                  SharedFolderCredentialsConfigs.Username,
                  SharedFolderCredentialsConfigs.Password);

                var result = Impersonation.RunAsUser(credentials, LogonType.Network, async () =>
                {
                    await File.WriteAllTextAsync(path, content);
                });

                return fileName;
            }

            return string.Empty;
        }

        public void Delete(string fileName, string folder)
        {
            if (!Directory.Exists($"{storage_path}//{folder}"))
                Directory.CreateDirectory($"{storage_path}//{folder}");

            full_path = $"{storage_path}/{folder}/{fileName}";

            // Code for delete fieles even in use
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Delete(full_path);
        }

        public void Delete(string fullPath)
        {
            // Code for delete fieles even in use
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var credentials = new UserCredentials(
                 SharedFolderCredentialsConfigs.Domain,
                 SharedFolderCredentialsConfigs.Username,
                 SharedFolderCredentialsConfigs.Password);

            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);
            });
        }

        public void DeleteDirectory(string folderpath)
        {
            // Code for delete fieles even in use
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var credentials = new UserCredentials(
                SharedFolderCredentialsConfigs.Domain,
                SharedFolderCredentialsConfigs.Username,
                SharedFolderCredentialsConfigs.Password);

            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                if (Directory.Exists(folderpath))
                    Directory.Delete(folderpath);
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}