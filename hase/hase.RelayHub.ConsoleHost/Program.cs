using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Framework.Service;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.RelayHub.ConsoleHost
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
            {
                Console.WriteLine("Starting SignalR Relay Server...");
                var relay = Startup.BuildWebHost(args);
                var ct = new CancellationToken();
                await relay.StartAsync(ct);
                Console.WriteLine("SignalR Relay started.");
                Console.WriteLine("Press <Enter> to stop relay.");
                Console.ReadLine();
                await relay.StopAsync(TimeSpan.FromSeconds(5));
                Console.WriteLine("Relay stopped.");
            }
            if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
            {
                var fsqServicePipeName = typeof(FileSystemQueryService).Name;
                var fsqProxyPipeName = ServiceTypesUtil.GetServiceProxyName(fsqServicePipeName);
                var calcServicePipeName = typeof(CalculatorService).Name;
                var calcProxyPipeName = ServiceTypesUtil.GetServiceProxyName(calcServicePipeName);

                Console.WriteLine("Starting Named Pipe Relay.");
                var fsqRelay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(fsqServicePipeName, fsqProxyPipeName);
                fsqRelay.StartAsync(); // not awaiting on purpose
                var calcRelay = new NamedPipeRelayHub<CalculatorRequest, CalculatorResponse>(calcServicePipeName, calcProxyPipeName);
                calcRelay.StartAsync(); // not awaiting on purpose
                Console.WriteLine("Named Pipe Relay started.");
                Console.WriteLine("Press <Enter> to stop relay.");
                Console.ReadLine();

                await fsqRelay.StopAsync();
                await calcRelay.StopAsync();

                Console.WriteLine("Relay stopped.");
            }

            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
    }
}
