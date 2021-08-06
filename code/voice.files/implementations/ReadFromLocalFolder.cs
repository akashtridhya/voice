using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace voice.files.implementations
{
    public class ReadFromLocalFolder : IDisposable
    {
        //public ReadFromLocalFolder(BaseUrlConfigs baseUrl) { }

        public Tuple<DataTable, string> ImportFileFromLocalFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filesList = Directory.GetFiles(folderPath, "*.csv",
                                         System.IO.SearchOption.TopDirectoryOnly).ToList();

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
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var filesList = Directory.GetFiles(folderPath, "*.csv", System.IO.SearchOption.TopDirectoryOnly).ToList();

            if (filesList == null) return null;
            if (filesList.Count == 0) return null;

            dynamic dataTeble, fileName, response = new List<Tuple<DataTable, string>>();
            foreach (var data in filesList)
            {
                dataTeble = GetDataTableFromCSVFile(data);
                fileName = data.Split(@"\").TakeLast(1).FirstOrDefault();
                response.Add(new Tuple<DataTable, string>(dataTeble, fileName));
            }

            return response;
        }

        public List<Tuple<DataTable, string>> ImportTSVFilesFromLocalFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var filesList = Directory.GetFiles(folderPath, "*.txt", System.IO.SearchOption.TopDirectoryOnly).Union(Directory.GetFiles(folderPath, "*.csv", System.IO.SearchOption.TopDirectoryOnly)).ToList();

            if (filesList == null) return null;
            if (filesList.Count == 0) return null;

            dynamic dataTeble, fileName, response = new List<Tuple<DataTable, string>>();
            foreach (var data in filesList)
            {
                dataTeble = GetDataTableFromTSVFile(data);
                fileName = data.Split(@"\").TakeLast(1).FirstOrDefault();
                response.Add(new Tuple<DataTable, string>(dataTeble, fileName));
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

        // Get Serialized string of TSV field
        private static DataTable GetDataTableFromTSVFile(string tsv_file_path)
        {
            DataTable tsvData = new DataTable();

            try
            {
                //tsv_file_path = !string.IsNullOrWhiteSpace(tsv_file_path.Substring(tsv_file_path.IndexOf(".txt") + ".txt".Length)) ? tsv_file_path.Replace(tsv_file_path.Substring(tsv_file_path.IndexOf(".txt") + ".txt".Length), "") : tsv_file_path;
                using TextFieldParser tsvReader = new TextFieldParser(tsv_file_path);
                tsvReader.SetDelimiters(new string[] { "\t" });
                tsvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = tsvReader.ReadFields();

                int duplicateCount = 1;
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;

                    // Check duplicate column name
                    if (!tsvData.Columns.Contains(datecolumn.ToString())) tsvData.Columns.Add(datecolumn);
                    else
                    {
                        tsvData.Columns.Add($"{datecolumn}_duplicate_{duplicateCount}");
                        duplicateCount++;
                    }
                }

                while (!tsvReader.EndOfData)
                {
                    string[] fieldData = tsvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }

                    tsvData.Rows.Add(fieldData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return tsvData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}