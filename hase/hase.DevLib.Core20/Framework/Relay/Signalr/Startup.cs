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
            .UseKestrel()
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
            {
                options.AddPolicy("any",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });
            //services.AddCors();
            services.AddSignalR();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("any");
            //app.UseCors(cors =>
            //{
            //    cors.AllowAnyHeader();
            //    cors.AllowAnyOrigin();
            //    cors.AllowAnyMethod();
            //});

            app.UseWebSockets();

            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalrRelayHub>("/route");
            });
        }
    }
}
