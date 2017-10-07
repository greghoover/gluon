using System;
//
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Server.Library;

namespace Gluon.Tester.Server.Conosle
{
    public class Program
    {
        public static readonly string HubChannelUri = "http://localhost:5000/messagehub";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Gluon Relay Tester Server...");

            var svcHost = new AppServiceHost<RpcService>("RpcServiceHost1", HubChannelUri);

            Console.WriteLine("Press Enter to stop Gluon Relay Tester Server: ");
            Console.ReadLine();
        }
    }
}
