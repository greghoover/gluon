using hase.DevLib.Contract;
using hase.DevLib.Contract.FileSystemQuery;
using hase.DevLib.Relay.NamedPipe;

namespace hase.DevLib.Service.FileSystemQuery
{
    public class FileSystemQueryProxy : IServiceProxy<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>
    {
        public FileSystemQueryResponse Execute(FileSystemQueryRequest request)
        {
            return new NamedPipeRelayProxyClient<FileSystemQueryService, FileSystemQueryProxy, FileSystemQueryRequest, FileSystemQueryResponse>().Execute(request);
        }
    }
}
