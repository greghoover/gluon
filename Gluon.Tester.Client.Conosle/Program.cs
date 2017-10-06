using System;
//
using Gluon.Relay.Signalr.Client;

namespace Gluon.Tester.Client.Conosle
{
    class Program
    {
        public static readonly string HubChannelUri = "http://localhost:5000/messagehub";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Gluon Relay Tester Client...");

            var proxyHub = new MessageHubClient("proxyHub", HubChannelUri);
            var workerHub = new MessageHubClient("workerHub", HubChannelUri);

            var proxy = new WorkerProxy(proxyHub);
            var worker = new Worker(workerHub);

            proxy.Invoke<string, string>("DoWork", "arg0");

            Console.WriteLine("Press Enter to stop Gluon Relay Tester Client: ");
            Console.ReadLine();
        }
    }
}
