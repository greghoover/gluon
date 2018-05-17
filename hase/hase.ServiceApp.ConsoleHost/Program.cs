using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting ServiceApp Dispatcher Console Host...");
            Console.WriteLine("Building service dispatchers.");
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IHostedService, SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
                    services.AddSingleton<IHostedService, SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>>();
                });

            Console.WriteLine("Starting service dispatchers.");
            await builder.RunConsoleAsync();

            Console.WriteLine();
            Console.WriteLine("Dispatchers stopped.");
            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
        //static async Task Main2(string[] args)
        //{
        //    var ct = new CancellationToken();
        //    var fsqDispatcher = default(IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>);
        //    var calcDispatcher = default(IRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>);

        //    if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
        //    {
        //        //var instanceId = "FileSystemQueryServiceHost";
        //        //var qs = $"?{ClientIdTypeEnum.ClientId}={instanceId}";
        //        //var subscriptionChannel = (@"http://localhost:5000/messagehub") + qs;
        //        //var proxy = new ServiceHostRelayProxy(instanceId, subscriptionChannel);

        //        fsqDispatcher = RelayDispatcher<SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
        //        calcDispatcher = RelayDispatcher<SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
        //    }
        //    if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
        //    {
        //        fsqDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
        //        calcDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
        //    }

        //    Console.WriteLine("Starting Service Dispatcher.");
        //    await fsqDispatcher.StartAsync(ct);
        //    await calcDispatcher.StartAsync(ct);
        //    Console.WriteLine("Service Dispatcher started.");

        //    Console.WriteLine("Press <Enter> to stop dispatcher.");
        //    Console.ReadLine();
        //    await fsqDispatcher.StopAsync(ct);
        //    await calcDispatcher.StopAsync(ct);
        //    Console.WriteLine("Dispatcher stopped.");

        //    Console.Write("Press <Enter> to close window.");
        //    Console.ReadLine();
        //}
    }
}
