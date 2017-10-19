using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;

namespace Gluon.Tester.Client.Library
{
    public class RpcClient : AppServiceClient, IRequestResponse<RpcRequestMsg, RpcResponseMsg>
    {
        public RpcClient(string instanceId, string subscriptionChannel) : base(instanceId, subscriptionChannel) { }

        public RpcResponseMsg RequestResponse(RpcRequestMsg request)
        {
            return RelayRequestResponse<RpcRequestMsg, RpcResponseMsg>(request);
        }
    }
}
