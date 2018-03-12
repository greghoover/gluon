using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using hase.DevLib.Framework.Relay.NamedPipe;
using System;
using hase.DevLib.Framework.Contract;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Service Dispatcher");

            var dispatcher = RelayDispatcherClientFactory<NamedPipeRelayDispatcherClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();

            dispatcher.Run();
        }
    }
}
