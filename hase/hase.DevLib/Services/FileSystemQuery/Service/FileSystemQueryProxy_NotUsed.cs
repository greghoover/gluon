//using hase.DevLib.Framework.Contract;
//using hase.DevLib.Services.FileSystemQuery.Contract;
//using hase.DevLib.Framework.Relay.NamedPipe;

//namespace hase.DevLib.Services.FileSystemQuery.Service
//{
//    public class FileSystemQueryProxy : IServiceProxy<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>//, IFileSystemQuery
//    {
//        public FileSystemQueryResponse Execute(FileSystemQueryRequest request)
//        {
//            var proxy = ServiceProxyFactory<NamedPipeRelayProxyClient, FileSystemQueryService, FileSystemQueryProxy, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
//        }
//    }
//}
