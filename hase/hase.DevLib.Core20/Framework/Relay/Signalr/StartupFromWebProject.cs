using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//namespace hase.Web
namespace hase.Relays.Signalr.Server
{
	public class StartupFromWebProject
	{
		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<StartupFromWebProject>();
		public static void CreateAndRunWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseStartup<StartupFromWebProject>()
			.Build()
			.Run();

		public StartupFromWebProject(IConfiguration configuration)
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

