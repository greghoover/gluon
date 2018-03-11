using hase.DevLib.Framework.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;

namespace hase.DevLib.Services.FileSystemQuery.Client
{
    public class FileSystemQuery : ServiceClient<FileSystemQueryService, FileSystemQueryProxy, FileSystemQueryRequest, FileSystemQueryResponse>, IFileSystemQuery
    {
        public FileSystemQuery() : this(isRemote: false)
        {
        }
        public FileSystemQuery(bool isRemote = false) : base(isRemote)
        {
        }

        public string DoesDirectoryExist(string folderPath)
        {
            var response = this.Execute(x => {
                x.QueryType = FileSystemQueryTypeEnum.DirectoryExists;
                x.FolderPath = folderPath;
            });
            return response.ResponseString;
        }
    }
}
