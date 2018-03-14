using hase.DevLib.Framework.Contract;
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
            var proxyPipeName = ServiceTypesUtil.GetServiceProxyName(servicePipeName);


            var relay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(servicePipeName, proxyPipeName);
            relay.Start();
            Console.WriteLine("Named Pipe Relay started.");
        }
    }
}
