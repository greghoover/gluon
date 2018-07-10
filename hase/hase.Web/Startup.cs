using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hase.Relays.Signalr.Server;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hase.Web
{
	/// <summary>
	/// Startup Class
	/// </summary>
	public class Startup
	{
		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.UseUrls(args)
			.Build();

		public static void BuildAndRunWebHost(string[] args) =>
			BuildWebHost(args).Run();

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		public Startup(IConfiguration cfg)
		{
			Configuration = cfg;
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Configures the services.
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
				options.AddPolicy("AllowCors",
					builder => builder.AllowAnyOrigin()
					.AllowCredentials()
					.AllowAnyHeader()
					.AllowAnyMethod()));

			services.AddSignalR();
		}

		/// <summary>
		/// Configures the specified application.
		/// </summary>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors("AllowCors");

			app.UseWebSockets();

			app.UseSignalR(routes =>
			{
				routes.MapHub<SignalrRelayHub>("/route");
			});
		}
	}
	public class Startup2
	{
		public static IWebHost BuildWebHost(string[] args)
		{
			var b = WebHost.CreateDefaultBuilder();
			b.UseKestrel();
			b.UseStartup<Startup>();
			b.CaptureStartupErrors(false);
			b.PreferHostingUrls(true);
			b.SuppressStatusMessages(false);
			b.UseUrls(string.Join(';', args));
			return b.Build();
		}

		public static void BuildAndRunWebHost(string[] args) =>
			BuildWebHost(args).Run();

		public Startup2(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.Configure<CookiePolicyOptions>(options =>
			//{
			//	// This lambda determines whether user consent for non-essential cookies is needed for a given request.
			//	options.CheckConsentNeeded = context => true;
			//	options.MinimumSameSitePolicy = SameSiteMode.None;
			//});


			services.AddCors(options => options.AddPolicy("CorsPolicy", builder => { builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().AllowAnyOrigin(); }));
			services.AddSignalR();
			//services.AddMvcCore();

			//services.AddMvc(); //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//services.AddCors(options =>
			//	options.AddPolicy("AllowCors",
			//		builder => builder.AllowAnyOrigin()
			//		.AllowCredentials()
			//		.AllowAnyHeader()
			//		.AllowAnyMethod()));

			//services.AddSignalR();
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
				app.UseExceptionHandler("/Error");
				//app.UseHsts();
			}

			//app.UseHttpsRedirection();
			//app.UseStaticFiles();
			//app.UseCookiePolicy();

			//app.UseMvc();

			//app.UseCors("AllowCors");
			//app.UseWebSockets();
			//app.UseSignalR(routes =>
			//{
			//	routes.MapHub<SignalrRelayHub>("/route");
			//});


			app.UseCors("CorsPolicy");
			app.UseSignalR(routes =>
			{
				routes.MapHub<SignalrRelayHub>("/route");
			});

			//app.UseMvc();

		}
	}
}
