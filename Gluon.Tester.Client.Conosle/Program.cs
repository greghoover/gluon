using Gluon.Relay.Contracts;
using Gluon.Tester.Client.Library;
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

            using (var fsqClient = new FileSystemQueryClient("FileSystemQueryClient", HubChannelUri))
            {
                while (true)
                {
                    Console.Write("Input file system path to check if exists: ");
                    var path = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(path))
                        break;

                    var request = new FileSystemQueryRqst(FileSystemQueryTypeEnum.DirectoryExists, path);
                    var response = fsqClient.RequestResponse(request, "FileSystemQueryServiceHost", ClientIdTypeEnum.ClientId);
                    Console.WriteLine("Single Request Response ========");
                    Console.WriteLine(response);

                    var gResponse = fsqClient.RequestGroupResponse(request, "FileSystemQueryServiceHost");
                    Console.WriteLine("Group Request Response ========");
                    foreach (var gRspn in gResponse)
                    {
                        Console.WriteLine(gRspn.Value);
                    }
                }
            }

            Console.WriteLine("Press Enter to stop Gluon Relay Tester Client: ");
            Console.ReadLine();
        }
    }
}
