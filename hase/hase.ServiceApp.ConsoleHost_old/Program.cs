using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using System.Threading.Tasks;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
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
            fsqDispatcher.StartAsync(); // not awaiting on purpose
            calcDispatcher.StartAsync(); // not awaiting on purpose
            Console.WriteLine("Service Dispatcher started.");

            Console.WriteLine("Press <Enter> to stop dispatcher.");
            Console.ReadLine();
            await fsqDispatcher.StopAsync();
            await calcDispatcher.StopAsync();
            Console.WriteLine("Dispatcher stopped.");

            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
    }
}
