using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var signalR = true;
            var namedPipe = !signalR;

            var fsqDispatcher = default(IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>);
            var calcDispatcher = default(IRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>);

            if (signalR)
            {
                //var instanceId = "FileSystemQueryServiceHost";
                //var qs = $"?{ClientIdTypeEnum.ClientId}={instanceId}";
                //var subscriptionChannel = (@"http://localhost:5000/messagehub") + qs;
                //var proxy = new ServiceHostRelayProxy(instanceId, subscriptionChannel);

                fsqDispatcher = RelayDispatcher<SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
                calcDispatcher = RelayDispatcher<SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
            }
            if (namedPipe)
            {
                fsqDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
                calcDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
            }

            Console.WriteLine("Starting Service Dispatcher");
            fsqDispatcher.StartAsync();
            calcDispatcher.StartAsync();
            Console.WriteLine("Service Dispatcher started.");

            Console.WriteLine("Press <Enter> to stop dispatcher.");
            Console.ReadLine();
            fsqDispatcher.StopAsync().Wait();
            calcDispatcher.StopAsync().Wait();
            Console.WriteLine("Dispatcher stopped.");

            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
    }
}
