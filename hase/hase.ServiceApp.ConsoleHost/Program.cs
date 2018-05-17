using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ct = new CancellationToken();
            var fsqDispatcher = default(IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>);
            var calcDispatcher = default(IRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>);

            if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
            {
                //var instanceId = "FileSystemQueryServiceHost";
                //var qs = $"?{ClientIdTypeEnum.ClientId}={instanceId}";
                //var subscriptionChannel = (@"http://localhost:5000/messagehub") + qs;
                //var proxy = new ServiceHostRelayProxy(instanceId, subscriptionChannel);

                fsqDispatcher = RelayDispatcher<SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
                calcDispatcher = RelayDispatcher<SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
            }
            if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
            {
                fsqDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
                calcDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
            }

            Console.WriteLine("Starting Service Dispatcher");
            await fsqDispatcher.StartAsync(ct);
            await calcDispatcher.StartAsync(ct);
            Console.WriteLine("Service Dispatcher started.");

            Console.WriteLine("Press <Enter> to stop dispatcher.");
            Console.ReadLine();
            await fsqDispatcher.StopAsync(ct);
            await calcDispatcher.StopAsync(ct);
            Console.WriteLine("Dispatcher stopped.");

            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
    }
}
