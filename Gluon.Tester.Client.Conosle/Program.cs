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

            var fsQueryClient = new FileSystemQueryClient("FileSystemQueryClient", HubChannelUri);
            while (true)
            {
                Console.Write("Input file system path to check if exists: ");
                var path = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(path))
                    break;

                var request = new FileSystemQueryRqst(FileSystemQueryTypeEnum.DirectoryExists, path);
                var response = fsQueryClient.RequestResponse(request, "FileSystemQueryServiceHost");
                Console.WriteLine(response);
            }

            fsQueryClient.DisposeAsync().Wait();
            Console.WriteLine("Press Enter to stop Gluon Relay Tester Client: ");
            Console.ReadLine();
        }
    }
}
