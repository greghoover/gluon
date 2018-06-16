//using hase.DevLib.Framework.Relay.NamedPipe;
using hase.AppServices.Calculator.Service;
using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.FileSystemQuery.Service;
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
					if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
					{
						Console.WriteLine($"{nameof(SignalrRelayDispatcher)}<{nameof(FileSystemQueryService)}>");
						services.AddSingleton<IHostedService, SignalrRelayDispatcher>(isp => new SignalrRelayDispatcher("FileSystemQueryService"));
						Console.WriteLine($"{nameof(SignalrRelayDispatcher)}<{nameof(CalculatorService)}>");
						services.AddSingleton<IHostedService, SignalrRelayDispatcher>(isp => new SignalrRelayDispatcher("CalculatorService"));
					}
					if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
					{
						//Console.WriteLine($"{nameof(NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>)}<{nameof(FileSystemQueryService)}>");
						//services.AddSingleton<IHostedService, NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
						//Console.WriteLine($"{nameof(NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>)}<{nameof(CalculatorService)}>");
						//services.AddSingleton<IHostedService, NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>>();
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
