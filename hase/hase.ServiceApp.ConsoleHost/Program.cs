using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using signalr = hase.DevLib.Framework.Relay._.Signalr;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using calcC = hase.DevLib.Services._.Calculator.Contract;
using calcS = hase.DevLib.Services._.Calculator.Service;
using fileC = hase.DevLib.Services._.FileSystemQuery.Contract;
using fileS = hase.DevLib.Services._.FileSystemQuery.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
                    if (RelayUtil.DispatchStrategy == DispatchStrategyEnum.Generics)
                    {
                        if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
                        {
                            //Console.WriteLine($"{nameof(SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>)}<{nameof(FileSystemQueryService)}>");
                            services.AddSingleton<IHostedService, SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
                            //Console.WriteLine($"{nameof(SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>)}<{nameof(CalculatorService)}>");
                            services.AddSingleton<IHostedService, SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>>();
                        }
                        if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
                        {
                            //Console.WriteLine($"{nameof(NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>)}<{nameof(FileSystemQueryService)}>");
                            services.AddSingleton<IHostedService, NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
                            //Console.WriteLine($"{nameof(NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>)}<{nameof(CalculatorService)}>");
                            services.AddSingleton<IHostedService, NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>>();
                        }
                    }
                    else if (RelayUtil.DispatchStrategy == DispatchStrategyEnum.Reflection)
                    {
                        if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
                        {
                            //Console.WriteLine($"{nameof(signalr.SignalrRelayDispatcher)}<{nameof(fileS.FileSystemQueryService)}>");
                            services.AddSingleton<IHostedService, signalr.SignalrRelayDispatcher>(isp => new signalr.SignalrRelayDispatcher("FileSystemQueryService"));
                            //Console.WriteLine($"{nameof(SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>)}<{nameof(CalculatorService)}>");
                            services.AddSingleton<IHostedService, signalr.SignalrRelayDispatcher>(isp => new signalr.SignalrRelayDispatcher("CalculatorService"));
                        }
                        if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
                        {
                            //Console.WriteLine($"{nameof(NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>)}<{nameof(FileSystemQueryService)}>");
                            services.AddSingleton<IHostedService, NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
                            //Console.WriteLine($"{nameof(NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>)}<{nameof(CalculatorService)}>");
                            services.AddSingleton<IHostedService, NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>>();
                        }
                    }
                });

            Console.WriteLine("Starting service dispatchers:");
            await builder.RunConsoleAsync();

            Console.WriteLine("Dispatchers stopped.");
            Console.WriteLine();
            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
    }
}
