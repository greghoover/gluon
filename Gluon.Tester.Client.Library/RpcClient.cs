using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;

namespace Gluon.Tester.Client.Library
{
    public class RpcClient : AppServiceClient, IRequestResponse<RpcRequestMsg, RpcResponseMsg>
    {
        public RpcClient() : base() { }
        public RpcClient(string instanceId, string subscriptionChannel) : base(instanceId, subscriptionChannel) { }

        public RpcResponseMsg DoRequestResponse(RpcRequestMsg request)
        {
            //this.Hub.InvokeAsync(CX.PushToClientsMethodName, request, null).Wait();
            //this.Hub.InvokeAsync(CX.RpcToClientMethodName, request.CorrelationId, request, null);
            var result = this.Hub.InvokeAsync<object>(CX.RpcToClientMethodName, request.CorrelationId, request, null).Result;

            var json = result as JObject;
            var response = json.ToObject<RpcResponseMsg>();
            return response;
        }
    }
}
