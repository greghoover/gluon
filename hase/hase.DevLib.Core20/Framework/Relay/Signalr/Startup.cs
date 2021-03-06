﻿using hase.DevLib.Framework.Relay.Hub;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace hase.Relays.Signalr.Server
{
	public class Startup
	{
		public static IWebHost BuildWebHost(string[] args, string[] urls = null)
		{
			var builder = WebHost.CreateDefaultBuilder(args);

			if (urls == null)
				urls = GetUrlsFromConfig();
			if (urls != null && urls.Length > 0)
				builder.UseUrls(urls);
			builder.UseStartup<Startup>();
			return builder.Build();
		}
		public static string[] GetUrlsFromConfig()
		{
			Console.WriteLine("Getting Configuration");
			var hostCfg = new RelayHubConfig().GetConfigSection();
			var hubCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.HubConfigSection);

			var signalrHubCfg = hubCfg.Get<SignalrRelayHubConfig>();
			var urls = signalrHubCfg?.HubUrl?.Select(x => (new Uri(x.GetLeftPart(UriPartial.Authority)))?.ToString())?.ToArray();
			return urls;
		}

		public static void BuildAndRunWebHost(string[] args) =>
			BuildWebHost(args).Run();
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddCors(options =>
				options.AddPolicy("AllowCors",
					builder => builder.AllowAnyOrigin()
					.AllowCredentials()
					.AllowAnyHeader()
					.AllowAnyMethod()));

			services.AddSignalR();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseStaticFiles(); // For the wwwroot folder

			//app.UseStaticFiles(new StaticFileOptions
			//{
			//    FileProvider = new PhysicalFileProvider(
			//        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "client", "hase-hub", "build")),
			//    RequestPath = ""
			//});

			app.UseDefaultFiles("");

			//app.UseDirectoryBrowser(new DirectoryBrowserOptions
			//{
			//    FileProvider = new PhysicalFileProvider(
			//        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "client", "hase-hub", "build")),
			//    RequestPath = ""
			//});

			//app.UseHttpsRedirection();
			app.UseMvc();

			app.UseCors("AllowCors");
			app.UseWebSockets();
			app.UseSignalR(routes =>
			{
				routes.MapHub<SignalrRelayHub>("/route");
			});
		}
	}
}
