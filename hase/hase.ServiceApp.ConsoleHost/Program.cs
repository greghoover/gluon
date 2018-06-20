//using hase.DevLib.Framework.Relay.NamedPipe;
//using hase.AppServices.Calculator.Service;
//using hase.AppServices.FileSystemQuery.Service;
using hase.DevLib.Framework.Client;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Contract;
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
			Console.WriteLine("Building service dispatchers.");
			var builder = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					// todo: 06/19/18 gph. Dynamically discover and load relay client assemblies.
					var dispatcherType = default(Type);
					switch (RelayUtil.RelayTypeDflt)
					{
						case RelayTypeEnum.SignalR:
							dispatcherType = typeof(SignalrRelayDispatcher);
							break;
						case RelayTypeEnum.NamedPipes:
						case RelayTypeEnum.NetMq:
							throw new NotSupportedException($"{RelayUtil.RelayTypeDflt} relay client is currently not supported.");
					}
					foreach (var svc in ServiceTypesUtil.GetServices())
					{
						var svcName = ContractUtil.EnsureServiceSuffix(svc.Desc);
						Console.WriteLine($"Configuring [{dispatcherType.Name}] for [{svcName}] processing.");
						services.AddSingleton(typeof(IHostedService), Activator.CreateInstance(dispatcherType, svcName));
					}
				});

			Console.WriteLine("Starting service dispatchers:");
			await builder.RunConsoleAsync();

			Console.WriteLine("Dispatchers stopped.");
			Console.WriteLine();
			Console.Write("Press <Enter> to close window.");
			Console.ReadLine();
		}
	}
}
