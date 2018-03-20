using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Core;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;

namespace hase.DevLib.Services.FileSystemQuery.Client
{
    public class FileSystemQuery : IFileSystemQuery
    {
        public bool DoesDirectoryExist(string folderPath, bool useLocalServiceInstance)
        {
            IService<FileSystemQueryRequest, FileSystemQueryResponse> fsqs = null;
            if (useLocalServiceInstance)
                fsqs = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateLocalInstance();
            else
                fsqs = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateConfiguredInstance();

            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };
            
            var response = fsqs.Execute(request).ResponseString;
            var result = bool.Parse(response);
            return result;
        }
    }
}
