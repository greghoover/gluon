using hase.DevLib.Framework.Contract;
using Microsoft.Extensions.Hosting;

namespace hase.DevLib.Framework.Relay
{
    public interface IRelayDispatcher<TService, TRequest, TResponse> : IHostedService
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        string Abbr { get; }
        //Task StartAsync();
        //Task StopAsync();
    }
}
