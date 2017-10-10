using Gluon.Relay.Contracts;
using Gluon.Tester.Contracts;
using Newtonsoft.Json.Linq;
using System;

namespace Gluon.Tester.Server.Library
{
    public class RpcService : IServiceType, IServiceType<RpcRequestMsg, RpcResponseMsg>
    {
        public RpcResponseMsg Execute(RpcRequestMsg inputMsg)
        {
            var outputMsg = new RpcResponseMsg(inputMsg, "gitrdone");
            Console.WriteLine(outputMsg);
            return outputMsg;
        }

        public object Execute(object msgIn)
        {
            var json = msgIn as JObject;
            var inputMsg = json.ToObject<RpcRequestMsg>();
            return Execute(inputMsg);
        }
    }
}
