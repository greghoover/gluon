using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;
using System.Threading.Tasks;

namespace Gluon.Tester.Client.Library
{
    public class FileSystemQueryClient : IRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>
    {
        AppServiceClient _appServiceClient;

        public FileSystemQueryClient(string instanceId, string subscriptionChannel)
        {
            _appServiceClient = new AppServiceClient(instanceId, subscriptionChannel);
        }

        public FileSystemQueryRspn RequestResponse(FileSystemQueryRqst request, string clientId)
        {
            return _appServiceClient.RelayRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>(request, clientId);
        }

        public Task DisposeAsync()
        {
            return _appServiceClient.HubClient.HubConnection.DisposeAsync();
        }
    }
}
