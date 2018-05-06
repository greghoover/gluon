using hase.DevLib.Framework.Relay.Signalr;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
    public class SignalrRelayFixture : IDisposable
    {
        //private SignalrRelayHub _signalrRelay = null;

        public SignalrRelayFixture()
        {
            Console.WriteLine("Starting SignalR Relay Server...");
            Task.Run(() =>
            {
                Startup.BuildAndRunWebHost(new string[] { });
            //Task.Delay(2000).Wait();
        });
            Console.WriteLine("Signalr Relay started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping SignalR Relay Server...");
            Task.Run(() =>
            {
            //if (_signalrRelay != null)
            //    _signalrRelay.StopAsync().Wait();
            //Task.Delay(2000).Wait();
        });
            Console.WriteLine("Signalr Relay stopped.");
        }
    }
}