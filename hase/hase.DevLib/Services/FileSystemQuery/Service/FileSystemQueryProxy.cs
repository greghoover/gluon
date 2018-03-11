using hase.DevLib.Framework.Contract;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Framework.Relay.NamedPipe;

namespace hase.DevLib.Services.FileSystemQuery.Service
{
    public class FileSystemQueryProxy : IServiceProxy<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>
    {
        public FileSystemQueryResponse Execute(FileSystemQueryRequest request)
        {
            return new NamedPipeRelayProxyClient<FileSystemQueryService, FileSystemQueryProxy, FileSystemQueryRequest, FileSystemQueryResponse>().Execute(request);
        }
    }
}
