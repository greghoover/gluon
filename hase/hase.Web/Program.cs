using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sr = hase.Relays.Signalr.Server;

namespace hase.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//sr.StartupFromWebProject.CreateAndRunWebHostBuilder(args);
			//sr.Startup.BuildAndRunWebHost(args);
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}

