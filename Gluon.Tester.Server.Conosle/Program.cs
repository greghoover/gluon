using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Server.Library;
using System;
using System.Threading.Tasks;

namespace Gluon.Tester.Server.Conosle
{
    public class Program
    {
        public static string HubChannelUri => "http://localhost:5000/messagehub";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Gluon Relay Tester Server...");

            //var svcHost = new AppServiceHost<RpcService>("RpcServiceHost1", HubChannelUri);
            var svcHost = new AppServiceHost<FileSystemQueryService>("FileSystemQueryServiceHost", HubChannelUri);

            //while (true)
            //    Task.Delay(200);

            Console.WriteLine("Press Enter to stop Gluon Relay Tester Server: ");
            Console.ReadLine();
        }
    }
}
