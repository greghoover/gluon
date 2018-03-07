using hase.DevLib.Contract.FileSystemQuery;
using hase.DevLib.Service.FileSystemQuery;

namespace hase.DevLib.Client.FileSystemQuery
{
    public class FileSystemQueryClient
    {
        public bool IsRemote { get; private set; }

        public FileSystemQueryClient(bool isRemote = false)
        {
            this.IsRemote = isRemote;
        }

        public string DoFileSystemQuery(string folderPath)
        {
            var service = FileSystemQueryFactory.CreateInstance(this.IsRemote);
            var request = new FileSystemQueryRequest(FileSystemQueryTypeEnum.DirectoryExists, folderPath);
            var response = service.Execute(request);
            return response.ResponseString;
        }
    }
}
