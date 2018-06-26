using hase.Relays.Signalr.Client;
using hase.AppServices.Calculator.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using hase.DevLib.Framework.Relay.Dispatcher;

namespace hase.DevLib.Tests.Fixtures
{
	public class Calculator_SignalrDispatcherFixture : IDisposable
	{
		IHost _host = null;

		public Calculator_SignalrDispatcherFixture()
		{
			Task.Run(() => StartServiceDispatcher()).Wait();
		}
		public void Dispose()
		{
			Task.Run(() => StopServiceDispatcher()).Wait(TimeSpan.FromSeconds(5));
		}
		void StartServiceDispatcher()
		{
			Console.WriteLine("Building service dispatcher.");
			_host = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					Console.WriteLine($"{nameof(SignalrRelayDispatcher)}<{nameof(CalculatorService)}>");
					var hostCfg = new RelayDispatcherConfig().GetConfigSection(nameof(CalculatorService));
					//var dispatcherCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.DispatcherConfigSection);
					var dispatcherCfg = new SignalrRelayDispatcherConfig();
					dispatcherCfg.HubUrl = new Uri("http://localhost:5150/route");
					services.AddSingleton<IHostedService, SignalrRelayDispatcher>(isp => new SignalrRelayDispatcher(nameof(CalculatorService), dispatcherCfg));
				}).Build();

			Console.WriteLine("Starting service dispatcher:");
			_host.StartAsync();
		}
		async void StopServiceDispatcher()
		{
			Console.WriteLine("Stopping SignalR relay server.");
			await _host.StopAsync(TimeSpan.FromSeconds(5));
			Console.WriteLine("SignalR relay server stopped.");
		}

	}
}