using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace voice.files
{
    public interface IExportManager : IDisposable
    {
        Task<string> WriteCsv<T>(IList<T> list, string fileName, string folder = null, string folderFullPath = null, bool includeHeader = true);

        Task<string> WriteTSV<T>(IList<T> list, string fileName, string folder = null, string folderFullPath = null, bool includeHeader = true);

        Tuple<DataTable, string> ReadFileAsString(string filePath);

        List<Tuple<DataTable, string>> ReadFilesAsString(string filePath);

        List<Tuple<DataTable, string>> ReadTSVFilesAsString(string filePath);
    }
}