using voice.models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace voice.files.implementations
{
    public class ExportManager : IExportManager
    {
        #region 1. Object Declaration And Constructor

        private const string DELIMITER = ",";

        private const string TSV_DELIMITER = "\t";

        private BaseUrlConfigs BaseUrlConfig { get; set; }

        public ExportManager(IOptions<BaseUrlConfigs> BaseUrlConfigOption)
        {
            BaseUrlConfig = BaseUrlConfigOption.Value;
        }

        #endregion 1. Object Declaration And Constructor

        #region 2. Csv

        public async Task<string> WriteCsv<T>(IList<T> list, string fileName, string folder = null, string folderFullPath = null, bool includeHeader = true)
        {
            var csv = this.Write(list, includeHeader);
            using var store_file = new StoreToLocalFolder(BaseUrlConfig);
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

        #region 3. TSV

        public async Task<string> WriteTSV<T>(
            IList<T> list,
            string fileName,
            string folder = null,
            string folderFullPath = null,
            bool includeHeader = true)
        {
            var csv = this.Write_TSV(list, includeHeader);
            using var store_file = new StoreToLocalFolder(BaseUrlConfig);
            return await store_file.Export(fileName, csv, folder: folder, folderFullPath: folderFullPath);
        }

        #region TSV Conversion Files

        private string Write_TSV<T>(IList<T> list, bool includeHeader = true)
        {
            var sb = new StringBuilder();
            var type = typeof(T);
            var properties = type.GetProperties();

            if (includeHeader)
            {
                sb.AppendLine(this.CreateTSVHeaderLine(properties));
            }

            foreach (var item in list)
            {
                sb.AppendLine(this.CreateTSVLine(item, properties));
            }

            return sb.ToString();
        }

        private string CreateTSVHeaderLine(PropertyInfo[] properties)
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

                this.CreateTsvStringItem(propertyValues, value);
            }

            return this.CreateTsvLine(propertyValues);
        }

        private string CreateTSVLine<T>(T item, PropertyInfo[] properties)
        {
            var propertyValues = new Collection<string>();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(item, null);

                if (prop.PropertyType == typeof(string))
                {
                    this.CreateTsvStringItem(propertyValues, value);
                }
                else if (prop.PropertyType == typeof(string[]))
                {
                    this.CreateTsvStringArrayItem(propertyValues, value);
                }
                else if (prop.PropertyType == typeof(List<string>))
                {
                    this.CreateTsvStringListItem(propertyValues, value);
                }
                else
                {
                    this.CreateTsvItem(propertyValues, value);
                }
            }

            return this.CreateTsvLine(propertyValues);
        }

        private string CreateTsvLine(IList<string> list)
        {
            return string.Join(TSV_DELIMITER, list);
        }

        private void CreateTsvItem(ICollection<string> propertyValues, object value)
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

        private void CreateTsvStringListItem(ICollection<string> propertyValues, object value)
        {
            //string formatString = "\"{0}\"";
            string formatString = "{0}";

            if (value != null)
            {
                value = this.CreateCsvLine((List<string>)value);
                propertyValues.Add(string.Format(formatString, this.ProcessTSVStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        private void CreateTsvStringArrayItem(ICollection<string> propertyValues, object value)
        {
            //string formatString = "\"{0}\"";
            string formatString = "{0}";
            if (value != null)
            {
                value = this.CreateCsvLine(((string[])value).ToList());
                propertyValues.Add(string.Format(formatString, this.ProcessTSVStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        private void CreateTsvStringItem(ICollection<string> propertyValues, object value)
        {
            //string formatString = "\"{0}\"";
            string formatString = "{0}";
            if (value != null)
            {
                propertyValues.Add(string.Format(formatString, this.ProcessTSVStringEscapeSequence(value)));
            }
            else
            {
                propertyValues.Add(string.Empty);
            }
        }

        private string ProcessTSVStringEscapeSequence(object value)
        {
            return value.ToString();
            //return value.ToString().Replace("\"", "\"\"");
        }

        #endregion TSV Conversion Files

        #endregion 3. TSV

        #region 4. Read File As String

        public Tuple<DataTable, string> ReadFileAsString(string filePath)
        {
            using var store_file = new ReadFromLocalFolder();
            return store_file.ImportFileFromLocalFolder(filePath);
        }

        public List<Tuple<DataTable, string>> ReadFilesAsString(string filePath)
        {
            using var store_file = new ReadFromLocalFolder();
            return store_file.ImportFilesFromLocalFolder(filePath);
        }

        public List<Tuple<DataTable, string>> ReadTSVFilesAsString(string filePath)
        {
            using var store_file = new ReadFromLocalFolder();
            return store_file.ImportTSVFilesFromLocalFolder(filePath);
        }

        #endregion 4. Read File As String

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