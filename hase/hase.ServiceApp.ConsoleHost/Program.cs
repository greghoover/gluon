using hase.DevLib.Contract.FileSystemQuery;
using hase.DevLib.Relay.NamedPipe;
using hase.DevLib.Service.FileSystemQuery;
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
