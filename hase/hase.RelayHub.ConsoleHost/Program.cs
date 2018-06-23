﻿using hase.DevLib.Framework.Relay.Hub;
//using hase.DevLib.Framework.Relay.NamedPipe;
using hase.Relays.Signalr.Client;
using hase.Relays.Signalr.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace hase.RelayHub.ConsoleHost
{
	class Program
	{
		private static async Task Main(string[] args)
		{
			Console.WriteLine("Starting Relay Server Console Host...");

			Console.WriteLine("Getting Configuration");
			var cb = new ConfigurationBuilder();
			var cfg = cb.AddJsonFile("appsettings.json")
				.AddCommandLine(args)
				.Build();
			var hostCfg = cfg.GetSection("RelayHub").Get<RelayHubConfig>();
			var hubCfg = cfg.GetSection(hostCfg.HubConfigSection);

			switch (hostCfg.HubTypeName)
			{
				case nameof(SignalrRelayHub):
					Console.WriteLine("Building SignalR relay server.");
					var relay = Startup.BuildWebHost(args);

					Console.WriteLine("Starting SignalR relay server.");
					await relay.StartAsync();
					Console.WriteLine("SignalR relay server started.");

					Console.WriteLine("Press Ctrl+C to shut down.");
					await relay.WaitForShutdownAsync();
					await relay.StopAsync(TimeSpan.FromSeconds(5));
					Console.WriteLine("SignalR relay server stopped.");
					break;
				//case named pipe...
				//    var fsqServicePipeName = typeof(FileSystemQueryService).Name;
				//    var fsqHubPipeName = ServiceTypesUtil.GetServiceHubName(fsqServicePipeName);
				//    var calcServicePipeName = typeof(CalculatorService).Name;
				//    var calcHubPipeName = ServiceTypesUtil.GetServiceHubName(calcServicePipeName);

				//    Console.WriteLine("Starting Named Pipe Relay.");
				//    var fsqRelay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(fsqServicePipeName, fsqHubPipeName);
				//    fsqRelay.StartAsync(); // not awaiting on purpose
				//    var calcRelay = new NamedPipeRelayHub<CalculatorRequest, CalculatorResponse>(calcServicePipeName, calcHubPipeName);
				//    calcRelay.StartAsync(); // not awaiting on purpose
				//    Console.WriteLine("Named Pipe Relay started.");
				//    Console.WriteLine("Press <Enter> to stop relay.");
				//    Console.ReadLine();

				//    await fsqRelay.StopAsync();
				//    await calcRelay.StopAsync();

				//    Console.WriteLine("Relay stopped.");
				//	break;
				default:
					throw new NotSupportedException($"{hostCfg.HubTypeName} relay server is currently not supported.");
			}

			Console.WriteLine();
			Console.Write("Press <Enter> to close window.");
			Console.ReadLine();
		}
	}
}
