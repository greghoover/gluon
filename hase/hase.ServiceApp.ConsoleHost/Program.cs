using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using hase.DevLib.Framework.Relay.NamedPipe;
using System;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Service Dispatcher");
            var dispatcher = new NamedPipeRelayDispatcherClient<FileSystemQueryService, FileSystemQueryProxy, FileSystemQueryRequest, FileSystemQueryResponse>();
            dispatcher.Run();
        }
    }
}
