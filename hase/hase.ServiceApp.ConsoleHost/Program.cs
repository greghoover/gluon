using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Dispatcher;
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

			// todo: 07/21/18 gph. Use a retry loop or Polly, instead of artificial wait.
			//var secondsToWait = 3;
			//Console.WriteLine($"Waiting {secondsToWait} seconds for relay to initialize.");
			//await Task.Delay(TimeSpan.FromSeconds(secondsToWait));

			Console.WriteLine("Getting Configuration");
			var hostCfg = new RelayDispatcherConfig().GetConfigSection();
			var dispatcherCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.DispatcherConfigSection);

			Console.WriteLine("Building service dispatchers.");
			var hb = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					var dispatcherType = RelayDispatcherBase.GetTypeFromAssembly(hostCfg.DispatcherClrType); 

					foreach (var name in hostCfg.ServiceTypeNames)
					{
						var serviceTypeName = ContractUtil.EnsureServiceSuffix(name);
						Console.WriteLine($"Configuring [{dispatcherType.Name}] for [{serviceTypeName}] processing.");
						services.AddSingleton(typeof(IHostedService), Activator.CreateInstance(dispatcherType, serviceTypeName, dispatcherCfg));
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
