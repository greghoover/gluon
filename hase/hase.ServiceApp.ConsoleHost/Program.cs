//using hase.DevLib.Framework.Relay.NamedPipe;
//using hase.AppServices.Calculator.Service;
//using hase.AppServices.FileSystemQuery.Service;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Dispatcher;
using hase.Relays.Signalr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hase.ServiceApp.ConsoleHost
{
	class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("Starting ServiceApp Dispatcher Console Host...");

			Console.WriteLine("Getting Configuration");
			var cb = new ConfigurationBuilder();
			var cfg = cb.AddJsonFile("appsettings.json")
				.AddCommandLine(args)
				.Build();
			var hostCfg = cfg.GetSection("ServiceDispatcher").Get<RelayDispatcherConfig>();
			var dispatcherCfg = cfg.GetSection(hostCfg.DispatcherConfigSection);

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
