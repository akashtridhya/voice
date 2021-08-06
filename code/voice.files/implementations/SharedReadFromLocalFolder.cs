using voice.models;
using Microsoft.VisualBasic.FileIO;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace voice.files.implementations
{
    public class SharedReadFromLocalFolder : IDisposable
    {
        private SharedFolderCredentialsConfigs SharedFolderCredentialsConfigs { get; set; }

        public SharedReadFromLocalFolder(BaseUrlConfigs baseUrl, SharedFolderCredentialsConfigs SharedFolderCredentialsConfigs)
        {
            this.SharedFolderCredentialsConfigs = SharedFolderCredentialsConfigs;
        }

        public Tuple<DataTable, string> ImportFileFromLocalFolder(string folderPath)
        {
            var credentials = new UserCredentials(
               SharedFolderCredentialsConfigs.Domain,
               SharedFolderCredentialsConfigs.Username,
               SharedFolderCredentialsConfigs.Password);

            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
            });

            var filesList = new List<string>();

            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                filesList = Directory.GetFiles(folderPath, "*.csv",
                                          System.IO.SearchOption.TopDirectoryOnly).ToList();
            });

            if (filesList == null) return null;
            if (filesList.Count == 0) return null;

            var filepath = filesList.OrderByDescending(x => x).FirstOrDefault(); // Here Logic will be updated for Specif file find

            var dataTeble = GetDataTableFromCSVFile(filepath);
            var fileName = filepath.Split(@"\").TakeLast(1).FirstOrDefault();
            var response = new Tuple<DataTable, string>(dataTeble, fileName);

            return response;
        }

        public List<Tuple<DataTable, string>> ImportFilesFromLocalFolder(string folderPath)
        {
            var credentials = new UserCredentials(
               SharedFolderCredentialsConfigs.Domain,
               SharedFolderCredentialsConfigs.Username,
               SharedFolderCredentialsConfigs.Password);

            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            });

            var filesList = new List<string>();
            Impersonation.RunAsUser(credentials, LogonType.Network, () =>
            {
                filesList = Directory.GetFiles(folderPath, "*.csv", System.IO.SearchOption.TopDirectoryOnly).ToList();
            });

            if (filesList == null) return null;
            if (filesList.Count == 0) return null;

            dynamic dataTeble, fileName, response = new List<Tuple<DataTable, string>>();
            foreach (var data in filesList)
            {
                Impersonation.RunAsUser(credentials, LogonType.Network, () =>
                {
                    dataTeble = GetDataTableFromCSVFile(data);
                    fileName = data.Split(@"\").TakeLast(1).FirstOrDefault();
                    response.Add(new Tuple<DataTable, string>(dataTeble, fileName));
                });
            }

            return response;
        }

        // Get Serialized string of CSV field
        private static DataTable GetDataTableFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();

            try
            {
                using TextFieldParser csvReader = new TextFieldParser(csv_file_path);
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();

                int duplicateCount = 1;
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;

                    // Check duplicate column name
                    if (!csvData.Columns.Contains(datecolumn.ToString())) csvData.Columns.Add(datecolumn);
                    else
                    {
                        csvData.Columns.Add($"{datecolumn}_duplicate_{duplicateCount}");
                        duplicateCount++;
                    }
                }

                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }

                    csvData.Rows.Add(fieldData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return csvData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}