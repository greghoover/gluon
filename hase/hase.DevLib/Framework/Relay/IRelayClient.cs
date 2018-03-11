using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay
{
    public interface IRelayClient<TService, TServiceProxy, TRequest, TResponse>
        where TServiceProxy : IServiceProxy<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
    }
}
