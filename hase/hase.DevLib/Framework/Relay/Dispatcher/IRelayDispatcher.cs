using Microsoft.Extensions.Hosting;

namespace hase.DevLib.Framework.Relay.Dispatcher
{
    public interface IRelayDispatcher : IHostedService
    {
        string Abbr { get; }
        //Task StartAsync();
        //Task StopAsync();
    }
}
