using hase.DevLib.Framework.Contract;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay
{
    public interface IRelayDispatcherClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        string Abbr { get; }
        Task StartAsync();
        Task StopAsync();
    }
}
