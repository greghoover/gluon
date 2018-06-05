using Microsoft.Extensions.Hosting;

namespace hase.DevLib.Framework.Relay._
{
    public interface IRelayDispatcher : IHostedService
    {
        string Abbr { get; }
        //Task StartAsync();
        //Task StopAsync();
    }
}
