using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;
using System;

namespace Gluon.Tester.Client.Conosle
{
    class Program
    {
        public static string HubChannelUri => "http://localhost:5000/messagehub";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Gluon Relay Tester Client...");

            //var rpcClient = new RpcClient("RpcClient1", HubChannelUri);
            //var request = new RpcRequestMsg("mySpecialRequest");
            //var response = rpcClient.RequestResponse(request);
            //Console.WriteLine(response.ToString());

            var appClient = new AppServiceClient("FileSystemQueryClient", HubChannelUri);
            while (true)
            {
                Console.Write("Input file system path to check if exists: ");
                var path = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(path))
                    break;

                var request = new FileSystemQueryRqst(FileSystemQueryTypeEnum.DirectoryExists, path);
                var response = appClient.RelayRequestResponse<FileSystemQueryRqst, FileSystemQueryRspn>(request, "FileSystemQueryServiceHost");
                Console.WriteLine(response);
            }

            Console.WriteLine("Press Enter to stop Gluon Relay Tester Client: ");
            Console.ReadLine();
        }
    }
}
