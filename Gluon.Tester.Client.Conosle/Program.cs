using System;
//
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Server.Library;

namespace Gluon.Tester.Client.Conosle
{
    class Program
    {
        public static readonly string HubChannelUri = "http://localhost:5000/messagehub";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Gluon Relay Tester Client...");

            var asm = typeof(Worker).Assembly;
            var proxyHub = new MessageHubClient("proxyHub", HubChannelUri, asm);
            var proxy = new WorkerProxy(proxyHub);

            proxy.Invoke<string, string>("DoWork", "WorkData");

            Console.WriteLine("Press Enter to stop Gluon Relay Tester Client: ");
            Console.ReadLine();
        }
    }
}
