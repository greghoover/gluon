using hase.DevLib.Contract.FileSystemQuery;
using hase.DevLib.Service.FileSystemQuery;

namespace hase.DevLib.Client.FileSystemQuery
{
    public class FileSystemQueryClient : ServiceClient<FileSystemQueryService, FileSystemQueryProxy, FileSystemQueryRequest, FileSystemQueryResponse>
    {
        public FileSystemQueryClient() : this(isRemote: false)
        {
        }
        public FileSystemQueryClient(bool isRemote = false) : base(isRemote)
        {
        }

        public string DoFileSystemQuery(string folderPath)
        {
            var response = this.Execute(x => {
                x.QueryType = FileSystemQueryTypeEnum.DirectoryExists;
                x.FolderPath = folderPath;
            });
            return response.ResponseString;
        }
    }
}
