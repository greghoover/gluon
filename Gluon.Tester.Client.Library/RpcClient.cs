using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;

namespace Gluon.Tester.Client.Library
{
    public class RpcClient : AppServiceClient, IRequestResponse<RpcRequestMsg, RpcResponseMsg>
    {
        public RpcClient() : base() { }
        public RpcClient(string instanceId, string subscriptionChannel) : base(instanceId, subscriptionChannel) { }

        public RpcResponseMsg DoRequestResponse(RpcRequestMsg request)
        {
            this.Hub.InvokeAsync("DoWork", request.RequestString).Wait();
            var response = new RpcResponseMsg(request, "gitrdone");
            return response;
        }
    }
}
