using Gluon.Relay.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gluon.Relay.Signalr.Client
{
    public abstract class RequestResponseClientBase<TRequestMsg, TResponseMsg> : IRequestResponse<TRequestMsg, TResponseMsg>, IDisposable
        where TRequestMsg : RelayMessageBase
        where TResponseMsg : RelayResponseBase<TRequestMsg>
    {
        protected AppServiceClient _appServiceClient;

        public RequestResponseClientBase(string instanceId, string subscriptionChannel)
        {
            _appServiceClient = new AppServiceClient(instanceId, subscriptionChannel);
        }

        public IDictionary<string, TResponseMsg> RequestGroupResponse(TRequestMsg request, string groupId)
        {
            return _appServiceClient.RelayRequestGroupResponse<TRequestMsg, TResponseMsg>(request, groupId);
        }
        public TResponseMsg RequestResponse(TRequestMsg request, string clientId, ClientIdTypeEnum clientIdType)
        {
            return _appServiceClient.RelayRequestResponse<TRequestMsg, TResponseMsg>(request, clientId, clientIdType);
        }

        public void Dispose()
        {
            _appServiceClient.Dispose();
        }
    }
}
