using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay
{
    public interface IRelayDispatcherClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        void Run();
        string Abbr { get; }
    }
}
