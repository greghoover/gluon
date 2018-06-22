﻿using hase.AppServices.FileSystemQuery.Service;
using hase.Relays.Signalr.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
	public class FileSystemQuery_SignalrDispatcherFixture : IDisposable
	{
		IHost _host = null;

		public FileSystemQuery_SignalrDispatcherFixture()
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
					Console.WriteLine($"{nameof(SignalrRelayDispatcher)}<{nameof(FileSystemQueryService)}>");
					// todo: 06/22/18 gph. Pass null to compile. Need to fix.
					services.AddSingleton<IHostedService, SignalrRelayDispatcher>(isp => new SignalrRelayDispatcher("FileSystemQueryService", null));
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