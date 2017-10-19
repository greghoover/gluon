using Gluon.Relay.Contracts;
using Gluon.Tester.Contracts;
using Newtonsoft.Json.Linq;
using System;

namespace Gluon.Tester.Server.Library
{
    public class RpcService : IServiceType, IServiceType<RpcRequestMsg, RpcResponseMsg>
    {
        public RpcResponseMsg Execute(RpcRequestMsg request)
        {
            var response = new RpcResponseMsg(request, "gitrdone");
            return response;
        }

        // todo: Eliminate the untyped Execute method, or at least move it to
        // a more centralized area. Don't want this noise in every service class.
        public void Execute(ICommunicationClient hub, object request)
        {
            var json = request as JObject;
            var requestMsg = json.ToObject<RpcRequestMsg>();
            var response = Execute(requestMsg);
            Console.WriteLine(response);
            hub.InvokeAsync(CX.RelayResponseMethodName, response.CorrelationId, response).Wait();
        }
    }
}
