using hase.DevLib.Framework.Relay;
//using hase.DevLib.Framework.Relay.NamedPipe;
using hase.Relays.Signalr.Client;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;

namespace hase.RelayHub.ConsoleHost
{
	class Program
	{
		private static async Task Main(string[] args)
		{
			Console.WriteLine("Starting Relay Server Console Host...");

			if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
			{
				Console.WriteLine("Building SignalR relay server.");
				var relay = Startup.BuildWebHost(args);

				Console.WriteLine("Starting SignalR relay server.");
				await relay.StartAsync();
				Console.WriteLine("SignalR relay server started.");

				Console.WriteLine("Press Ctrl+C to shut down.");
				await relay.WaitForShutdownAsync();
				await relay.StopAsync(TimeSpan.FromSeconds(5));
				Console.WriteLine("SignalR relay server stopped.");
			}

			//if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
			//{
			//    var fsqServicePipeName = typeof(FileSystemQueryService).Name;
			//    var fsqProxyPipeName = ServiceTypesUtil.GetServiceProxyName(fsqServicePipeName);
			//    var calcServicePipeName = typeof(CalculatorService).Name;
			//    var calcProxyPipeName = ServiceTypesUtil.GetServiceProxyName(calcServicePipeName);

			//    Console.WriteLine("Starting Named Pipe Relay.");
			//    var fsqRelay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(fsqServicePipeName, fsqProxyPipeName);
			//    fsqRelay.StartAsync(); // not awaiting on purpose
			//    var calcRelay = new NamedPipeRelayHub<CalculatorRequest, CalculatorResponse>(calcServicePipeName, calcProxyPipeName);
			//    calcRelay.StartAsync(); // not awaiting on purpose
			//    Console.WriteLine("Named Pipe Relay started.");
			//    Console.WriteLine("Press <Enter> to stop relay.");
			//    Console.ReadLine();

			//    await fsqRelay.StopAsync();
			//    await calcRelay.StopAsync();

			//    Console.WriteLine("Relay stopped.");
			//}

			Console.WriteLine();
			Console.Write("Press <Enter> to close window.");
			Console.ReadLine();
		}
	}
}
