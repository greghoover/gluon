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
            hub.InvokeAsync(CX.RpcFromClientMethodName, request.CorrelationId, response).Wait();

            return response;
        }

        public object Execute(ICommunicationClient hub, object msgIn)
        {
            var json = msgIn as JObject;
            var request = json.ToObject<RpcRequestMsg>();
            return Execute(hub, request);
        }
    }
}
