using hase.Relays.Signalr.Client;
using hase.Relays.Signalr.Server;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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

		private Uri GetBaseUri(Uri url)
		{
			return new Uri($"{url.GetLeftPart(UriPartial.Authority)}");
		}
		async void StartRelayServer()
		{
			Console.WriteLine("Building SignalR relay server.");
			var hubCfg = new SignalrRelayHubConfig().GetConfigSection();
			var hubUrls = new List<Uri>(hubCfg.HubUrl.Select((uri) => GetBaseUri(uri)));
			_relay = Startup.BuildWebHost(hubUrls.Select(x => x.ToString()).ToArray());

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