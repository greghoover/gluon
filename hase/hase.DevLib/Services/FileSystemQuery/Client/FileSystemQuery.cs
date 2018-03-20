using hase.DevLib.Framework.Core;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;

namespace hase.DevLib.Services.FileSystemQuery.Client
{
    public class FileSystemQuery : IFileSystemQuery
    {
        public string DoesDirectoryExist(string folderPath)
        {
            var fsqs = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateConfiguredInstance();

            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };
            return fsqs.Execute(request).ResponseString;
        }
    }
}
