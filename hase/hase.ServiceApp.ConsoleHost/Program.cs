//using hase.DevLib.Framework.Relay.NamedPipe;
//using hase.AppServices.Calculator.Service;
//using hase.AppServices.FileSystemQuery.Service;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Dispatcher;
using hase.Relays.Signalr.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace hase.ServiceApp.ConsoleHost
{
	class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("Starting ServiceApp Dispatcher Console Host...");

			// todo: 07/21/18 gph. Use a retry loop or Polly, so instead of artificial wait.
			var secondsToWait = 3;
			Console.WriteLine($"Waiting {secondsToWait} seconds for relay to initialize.");
			await Task.Delay(TimeSpan.FromSeconds(secondsToWait));

			Console.WriteLine("Getting Configuration");
			var hostCfg = new RelayDispatcherConfig().GetConfigSection();
			var dispatcherCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.DispatcherConfigSection);

			Console.WriteLine("Building service dispatchers.");
			var hb = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					// todo: 06/19/18 gph. Dynamically discover and load relay client assemblies.
					var dispatcherType = default(Type);
					switch (hostCfg.DispatcherTypeName)
					{
						case nameof(SignalrRelayDispatcher):
							dispatcherType = typeof(SignalrRelayDispatcher);
							break;
						//case nameof(NamedPipeRelayDispatcher):
						//case nameof(NetMqRelayDispatcher):
						default:
							throw new NotSupportedException($"{hostCfg.DispatcherTypeName} relay client is currently not supported.");
					}

					var serviceTypes = hostCfg.ServiceTypeNames;
					foreach (var svcTyp in serviceTypes)
					{
						var serviceType = ContractUtil.EnsureServiceSuffix(svcTyp);
						Console.WriteLine($"Configuring [{dispatcherType.Name}] for [{serviceType}] processing.");
						services.AddSingleton(typeof(IHostedService), Activator.CreateInstance(dispatcherType, serviceType, dispatcherCfg));
					}
				});

			Console.WriteLine("Starting service dispatchers:");
			await hb.RunConsoleAsync();

			Console.WriteLine("Dispatchers stopped.");
			Console.WriteLine();
			Console.Write("Press <Enter> to close window.");
			Console.ReadLine();
		}
	}
}
