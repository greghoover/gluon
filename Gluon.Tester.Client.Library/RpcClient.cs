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

        public RpcResponseMsg RequestResponse(RpcRequestMsg request)
        {
            var result = this.Hub.InvokeAsync<object>(CX.RequestToClientMethodName, request.CorrelationId, request, null).Result;

            var json = result as JObject;
            var response = json.ToObject<RpcResponseMsg>();
            return response;
        }
    }
}
