using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;
using System;
using System.Collections.Generic;

namespace Gluon.Tester.Client.Library
{
    public class FileSystemQueryClient : IRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>, IDisposable
    {
        AppServiceClient _appServiceClient;

        public FileSystemQueryClient(string instanceId, string subscriptionChannel)
        {
            _appServiceClient = new AppServiceClient(instanceId, subscriptionChannel);
        }

        public Dictionary<string, FileSystemQueryRspn> RequestGroupResponse(FileSystemQueryRqst request, string groupId)
        {
            return _appServiceClient.RelayRequestGroupResponse<FileSystemQueryRqst, FileSystemQueryRspn>(request, groupId);
        }
        public FileSystemQueryRspn RequestResponse(FileSystemQueryRqst request, string clientId, ClientIdTypeEnum clientIdType)
        {
            return _appServiceClient.RelayRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>(request, clientId, clientIdType);
        }

        public void Dispose()
        {
            _appServiceClient.HubClient.HubConnection.DisposeAsync().Wait();
        }
    }
}
