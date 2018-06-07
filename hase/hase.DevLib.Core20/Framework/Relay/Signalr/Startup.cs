using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hase.DevLib.Framework.Relay.Signalr
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
}
