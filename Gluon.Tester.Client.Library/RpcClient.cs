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
            this.Hub.InvokeAsync(CX.PushToClientMethodName, request, null).Wait();

            // Fudge the respone message for now b/c getting return message back not yet implemented.
            var response = new RpcResponseMsg(request, "gitrdone (fudged)");

            return response;
        }
    }
}
