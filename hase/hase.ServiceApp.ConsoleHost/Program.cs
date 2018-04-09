using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using Gluon.Relay.Signalr.Client;
using Gluon.Relay.Contracts;

namespace hase.ServiceApp.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var instanceId = "FileSystemQueryServiceHost";
            var qs = $"?{ClientIdTypeEnum.ClientId}={instanceId}";
            var subscriptionChannel = (@"http://localhost:5000/messagehub") + qs;
            var proxy = new ServiceHostRelayProxy(instanceId, subscriptionChannel);

            var fsqDispatcher = RelayDispatcherClient<NamedPipeRelayDispatcherClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
            var calcDispatcher = RelayDispatcherClient<NamedPipeRelayDispatcherClient<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();

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
