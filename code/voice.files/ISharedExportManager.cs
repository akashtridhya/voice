using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace voice.files
{
    public interface ISharedExportManager : IDisposable
    {
        Task<string> WriteCsv<T>(IList<T> list, string fileName, string folder = null, string folderFullPath = null, bool includeHeader = true);

        Tuple<DataTable, string> ReadFileAsString(string filePath);

        List<Tuple<DataTable, string>> ReadFilesAsString(string filePath);

        void Move(string sourceFilePath, string destinationFilePath);

        void MoveDirectory(string sourceFilePath, string destinationFilePath);

        void MoveFoldersUsingCommand(string sourceFilePath, string destinationFilePath);
    }
}