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
            Console.WriteLine("Starting Named Pipe Relay.");

            var servicePipeName = typeof(FileSystemQueryService).Name;
            var proxyPipeName = servicePipeName;
            if (proxyPipeName.EndsWith("Service"))
                proxyPipeName = proxyPipeName.Replace("Service", "Proxy");


            var relay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(servicePipeName, proxyPipeName);
            relay.Start();
            Console.WriteLine("Named Pipe Relay started.");
        }
    }
}
