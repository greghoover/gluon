using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
    public class Calculator_SignalrDispatcherFixture : IDisposable
    {
        private CancellationToken _ct = new CancellationToken();
        private IRelayDispatcher _dispatcher = null;

        public Calculator_SignalrDispatcherFixture()
        {
            Console.WriteLine("Starting Service Dispatcher");
            Task.Run(async () =>
            {
                _dispatcher = new SignalrRelayDispatcher("CalculatorService");
                await _dispatcher.StartAsync(_ct);
            });
            Console.WriteLine("Service Dispatcher started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping Dispatcher.");
            Task.Run(async () =>
            {
                if (_dispatcher != null)
                    await _dispatcher.StopAsync(_ct);
            });
            Console.WriteLine("Dispatcher stopped.");
        }
    }
}