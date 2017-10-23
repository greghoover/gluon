using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;
using System;

namespace Gluon.Tester.Client.Library
{
    public class FileSystemQueryClient : IRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>, IDisposable
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

        public void Dispose()
        {
            _appServiceClient.HubClient.HubConnection.DisposeAsync().Wait();
        }
    }
}
