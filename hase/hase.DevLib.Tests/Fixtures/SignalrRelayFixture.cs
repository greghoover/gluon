using hase.Relays.Signalr.Client;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
    public class SignalrRelayFixture : IDisposable
    {
        IWebHost _relay = null;

        public SignalrRelayFixture()
        {
            Task.Run(() => StartRelayServer()).Wait();
        }
        public void Dispose()
        {
            Task.Run(() => StopRelayServer()).Wait();
        }

        async void StartRelayServer()
        {
            Console.WriteLine("Building SignalR relay server.");
            // todo: 06/07/18 gph. Get uri from configuration.
            _relay = Startup.BuildWebHost(new string[]{ "http://127.0.0.1:5000" });

            Console.WriteLine("Starting SignalR relay server.");
            await _relay.StartAsync();
            Console.WriteLine("SignalR relay server started.");

            //Console.WriteLine("Waiting for Dispose to be called for shutdown.");
            //await _relay.WaitForShutdownAsync();
        }
        async void StopRelayServer()
        {
            Console.WriteLine("Stopping SignalR relay server.");
            await _relay.StopAsync(TimeSpan.FromSeconds(5));
            Console.WriteLine("SignalR relay server stopped.");
        }
    }
}