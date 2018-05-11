using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
    public class Calculator_SignalrDispatcherFixture : IDisposable
    {
        private IRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse> _dispatcher = null;

        public Calculator_SignalrDispatcherFixture()
        {
            Console.WriteLine("Starting Service Dispatcher");
            Task.Run(() =>
            {
                _dispatcher = RelayDispatcher<SignalrRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
                _dispatcher.StartAsync(); // not awaiting on purpose
            //Task.Delay(2000).Wait();
        });
            Console.WriteLine("Service Dispatcher started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping Dispatcher.");
            Task.Run(async () =>
            {
                if (_dispatcher != null)
                    await _dispatcher.StopAsync();
            //Task.Delay(2000).Wait();
        });
            Console.WriteLine("Dispatcher stopped.");
        }
    }
}