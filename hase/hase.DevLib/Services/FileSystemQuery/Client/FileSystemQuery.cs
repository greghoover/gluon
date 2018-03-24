using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;

namespace hase.DevLib.Services.FileSystemQuery.Client
{
    public class FileSystemQuery : IFileSystemQuery
    {
        private IService<FileSystemQueryRequest, FileSystemQueryResponse> _service = null;
        private FileSystemQuery() { }
        //public FileSystemQuery()
        //{
        //    this._service = FileSystemQuery.NewLocal()._service;
        //}
        private FileSystemQuery(IService<FileSystemQueryRequest, FileSystemQueryResponse> service)
        {
            this._service = service;
        }

        public static FileSystemQuery NewLocal()
        {
            var service = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.NewLocal();
            return new FileSystemQuery(service);
        }
        public static FileSystemQuery NewWithNamedPipeProxy()
        {
            var service = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.NewProxied<NamedPipeRelayProxyClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
            return new FileSystemQuery(service);
        }

        public bool DoesDirectoryExist(string folderPath)
        {
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = _service.Execute(request).ResponseString;
            var result = bool.Parse(response);
            return result;
        }
    }
}
