using hase.DevLib.Contract.FileSystemQuery;
using hase.DevLib.Service.FileSystemQuery;

namespace hase.DevLib.Client.FileSystemQuery
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
