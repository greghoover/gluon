using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Service Dispatcher");

            var dispatcher = RelayDispatcherClient<NamedPipeRelayDispatcherClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();

            dispatcher.Run();
        }
    }
}
