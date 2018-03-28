using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;

namespace hase.RelayHub.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var servicePipeName = typeof(FileSystemQueryService).Name;
            var proxyPipeName = ServiceTypesUtil.GetServiceProxyName(servicePipeName);

            Console.WriteLine("Starting Named Pipe Relay.");
            var relay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(servicePipeName, proxyPipeName);
            relay.StartAsync();
            Console.WriteLine("Named Pipe Relay started.");

            Console.WriteLine("Press <Enter> to stop relay.");
            Console.ReadLine();
            relay.StopAsync().Wait();
            Console.WriteLine("Relay stopped.");

            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
    }
}
