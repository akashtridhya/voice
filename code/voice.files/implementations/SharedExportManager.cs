using voice.models;
using Microsoft.Extensions.Options;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace voice.files.implementations
{
    public class SharedExportManager : ISharedExportManager
    {
        #region 1. Object Declaration And Constructor

        private const string DELIMITER = ",";
        private BaseUrlConfigs BaseUrlConfig { get; set; }

        private SharedFolderCredentialsConfigs SharedFolderCredentialsConfigs { get; set; }

        public SharedExportManager(IOptions<BaseUrlConfigs> BaseUrlConfigOption,  IOptions<SharedFolderCredentialsConfigs> sharedFolderCredentialsConfigsOptions)
        {
            BaseUrlConfig = BaseUrlConfigOption.Value;
            SharedFolderCredentialsConfigs = sharedFolderCredentialsConfigsOptions.Value;
        }
        #endregion 1. Object Declaration And Constructor

        #region 2. Csv

        public async Task<string> WriteCsv<T>(IList<T> list, string fileName, string folder = null, string folderFullPath = null, bool includeHeader = true)
        {
            var csv = this.Write(list, includeHeader);
            using var store_file = new SharedStoreToLocalFolder(BaseUrlConfig, SharedFolderCredentialsConfigs);
            return await store_file.Export(fileName, csv, folder: folder, folderFullPath: folderFullPath);
        }

        #region CSV Conversion Files

        private string Write<T>(IList<T> list, bool includeHeader = true)
        {
            var sb = new StringBuilder();
            var type = typeof(T);
            var properties = type.GetProperties();

            if (includeHeader)
            {
                sb.AppendLine(this.CreateCsvHeaderLine(properties));
            }

            foreach (var item in list)
            {
                sb.AppendLine(this.CreateCsvLine(item, properties));
            }

            return sb.ToString();
        }

        private string CreateCsvHeaderLine(PropertyInfo[] properties)
        {
            var propertyValues = new Collection<string>();

            foreach (var prop in properties)
            {
                string value = prop.Name;

                var attribute = prop.GetCustomAttribute(typeof(DisplayAttribute));
                if (attribute != null)
                {
                    value = (attribute as DisplayAttribute).Name;
                }

                this.CreateCsvStringItem(propertyValues, value);
            }

            return this.CreateCsvLine(propertyValues);
        }

        private string CreateCsvLine<T>(T item, PropertyInfo[] properties)
        {
            var propertyValues = new Collection<string>();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(item, null);

                if (prop.PropertyType == typeof(string))
                {
                    this.CreateCsvStringItem(propertyValues, value);
                }
                else if (prop.PropertyType == typeof(string[]))
                {
                    this.CreateCsvStringArrayItem(propertyValues, value);
                }
                else if (prop.PropertyType == typeof(List<string>))
                {
                    this.CreateCsvStringListItem(propertyValues, value);
                }
                else
                {
                    this.CreateCsvItem(propertyValues, value);
                }
            }

            return this.CreateCsvLine(propertyValues);
        }

        private string CreateCsvLine(IList<string> list)
        {
            return string.Join(DELIMITER, list);
        }

        private void CreateCsvItem(ICollection<string> propertyValues, object value)
        {
            if (value != null)
            {
                propertyValues.Add(value.ToString());
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        private void CreateCsvStringListItem(ICollection<string> propertyValues, object value)
        {
            string formatString = "\"{0}\"";
            if (value != null)
            {
                value = this.CreateCsvLine((List<string>)value);
                propertyValues.Add(string.Format(formatString, this.ProcessStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        private void CreateCsvStringArrayItem(ICollection<string> propertyValues, object value)
        {
            string formatString = "\"{0}\"";
            if (value != null)
            {
                value = this.CreateCsvLine(((string[])value).ToList());
                propertyValues.Add(string.Format(formatString, this.ProcessStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        private void CreateCsvStringItem(ICollection<string> propertyValues, object value)
        {
            string formatString = "\"{0}\"";
            if (value != null)
            {
                propertyValues.Add(string.Format(formatString, this.ProcessStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        private string ProcessStringEscapeSequence(object value)
        {
            return value.ToString().Replace("\"", "\"\"");
        }

        #endregion CSV Conversion Files

        #endregion 2. Csv

        #region 3. Read File As String

        public Tuple<DataTable, string> ReadFileAsString(string filePath)
        {
            using var store_file = new SharedReadFromLocalFolder(BaseUrlConfig, SharedFolderCredentialsConfigs);
            return store_file.ImportFileFromLocalFolder(filePath);
        }

        public List<Tuple<DataTable, string>> ReadFilesAsString(string filePath)
        {
            using var store_file = new SharedReadFromLocalFolder(BaseUrlConfig, SharedFolderCredentialsConfigs);
            return store_file.ImportFilesFromLocalFolder(filePath);
        }

        #endregion 3. Read File As String

        public void Move(string sourceFilePath, string destinationFilePath)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var credentials = new UserCredentials(
              SharedFolderCredentialsConfigs.Domain,
              SharedFolderCredentialsConfigs.Username,
              SharedFolderCredentialsConfigs.Password);

            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                if (!File.Exists(sourceFilePath)) return;

                File.Move(sourceFilePath, destinationFilePath);
            });
        }

        public void MoveDirectory(string sourceFilePath, string destinationFilePath)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var credentials = new UserCredentials(
               SharedFolderCredentialsConfigs.Domain,
               SharedFolderCredentialsConfigs.Username,
               SharedFolderCredentialsConfigs.Password);

            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                if (!Directory.Exists(sourceFilePath)) return;

                Directory.Move(sourceFilePath, destinationFilePath);
            });
        }

        public void MoveFoldersUsingCommand(string sourceFilePath, string destinationFilePath)
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe", "/C" + @$"MOVE {sourceFilePath}* {destinationFilePath}")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };
            var process = Process.Start(processStartInfo);
            process.WaitForExit(20000);
            process.Close();
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