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
		IWebHost _relay = null;

		public SignalrRelayFixture() { }

		public void Dispose()
		{
			Task.Run(() => StopRelayServer()).Wait();
		}

		private Uri GetBaseUri(Uri url)
		{
			return new Uri($"{url.GetLeftPart(UriPartial.Authority)}");
		}
		public async void StartRelayServer(SignalrRelayHubConfig signalrHubCfg)
		{
			try
			{
				var urls = signalrHubCfg.HubUrl.Select(x => (new Uri(x.GetLeftPart(UriPartial.Authority))).ToString()).ToArray();

				Console.WriteLine("Building SignalR relay server.");
				_relay = Startup.BuildWebHost(urls);

				Console.WriteLine("Starting SignalR relay server.");
				await _relay.StartAsync();
				Console.WriteLine("SignalR relay server started.");
			}
			catch { }
		}
		async void StopRelayServer()
		{
			try
			{
				Console.WriteLine("Stopping SignalR relay server.");
				await _relay.StopAsync(TimeSpan.FromSeconds(5));
				Console.WriteLine("SignalR relay server stopped.");
			}
			catch { }
		}
	}
}