using hase.Relays.Signalr.Client;
using hase.Relays.Signalr.Server;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
	public class SignalrRelayFixture : IDisposable
	{
		public IWebHost Relay { get; private set; } = null;

		public SignalrRelayFixture() { }

		public void Dispose()
		{
			Task.Run(() => StopRelayServer()).Wait();
		}

		public Uri GetBaseUri(Uri url)
		{
			return new Uri($"{url.GetLeftPart(UriPartial.Authority)}");
		}
		public async void StartRelayServer(SignalrRelayHubConfig signalrHubCfg)
		{
			if (Relay != null)
				return; // has been started already

            try
            {
                var urls = signalrHubCfg.HubUrl.Select(x => (new Uri(x.GetLeftPart(UriPartial.Authority))).ToString()).ToArray();

                Console.WriteLine("Building SignalR relay server.");
                Relay = Startup.BuildWebHost(new string[] { string.Empty }, urls);

                Console.WriteLine("Starting SignalR relay server.");
                await Relay.StartAsync();
                Console.WriteLine("SignalR relay server started.");
            }
            catch (Exception ex) { }
		}
		async void StopRelayServer()
		{
			try
			{
				Console.WriteLine("Stopping SignalR relay server.");
				await Relay.StopAsync(TimeSpan.FromSeconds(5));
				Console.WriteLine("SignalR relay server stopped.");
			}
			catch { }
		}
	}
}