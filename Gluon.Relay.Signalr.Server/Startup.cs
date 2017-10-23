using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Gluon.Relay.Signalr.Server
{
    public class Startup
    {
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        public static void BuildAndRunWebHost(string[] args) =>
            BuildWebHost(args).Run();

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseQueryStringParameters();
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("messagehub");
            });
        }
    }

    public static class StartupExtensions
    {
        private static Dictionary<string, string> ParseQueryString(HttpContext context)
        {
            var dict = new Dictionary<string, string>();

            if (!context.Request.QueryString.HasValue)
                return dict;

            var qs = context.Request.QueryString.Value;
            if (qs.StartsWith("?"))
                qs = qs.Remove(0, 1);
            var pairs = qs.Split('&');

            foreach (var pair in pairs)
            {
                var tokens = pair.Split('=');
                var key = tokens[0];
                var vlu = tokens[1];
                dict.Add(key, vlu);
            }

            return dict;
        }

        public static void UseQueryStringParameters(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var dict = ParseQueryString(context);

                //if (string.IsNullOrWhiteSpace(context.Request.Headers[CX.AuthorizationKey]))
                //{
                //    var bt = dict.GetValueOrDefault(CX.AuthorizationKey);
                //    context.Request.Headers.Add(CX.AuthorizationKey, new[] { $"Bearer {bt}" });
                //}

                foreach (var kvp in dict)
                {
                    context.Items.Add(kvp.Key, kvp.Value);
                }

                await next.Invoke();
            });
        }
    }

}

