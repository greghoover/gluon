using System;
//
using Gluon.Relay.Contracts;

namespace Gluon.Tester.Server.Library
{
    public class RpcService : IServiceType, IServiceType<string, string>
    {
        public string Execute(string inputMsg)
        {
            var outputMsg = $"Input message '{inputMsg.ToString()}' processed.";
            Console.WriteLine(outputMsg);
            return outputMsg;
        }

        public object Execute(object inputMsg)
        {
            return Execute(inputMsg.ToString());
        }
    }
}
