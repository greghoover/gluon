using Gluon.Relay.Contracts;
using Gluon.Tester.Contracts;
using Newtonsoft.Json.Linq;
using System;

namespace Gluon.Tester.Server.Library
{
    public class RpcService : IServiceType, IServiceType<RpcRequestMsg, RpcResponseMsg>
    {
        public RpcResponseMsg Execute(ICommunicationClient hub, RpcRequestMsg request)
        {
            var response = new RpcResponseMsg(request, "gitrdone");
            Console.WriteLine(response);
            hub.InvokeAsync(CX.ResponseFromClientMethodName, response.CorrelationId, response).Wait();

            return response;
        }

        // todo: Eliminate the untyped Execute method, or at least move it to
        // a more centralized area. Don't want this noise in every service class.
        public object Execute(ICommunicationClient hub, object request)
        {
            var json = request as JObject;
            var requestMsg = json.ToObject<RpcRequestMsg>();
            return Execute(hub, requestMsg);
        }
    }
}
