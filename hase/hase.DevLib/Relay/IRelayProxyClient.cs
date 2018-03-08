using hase.DevLib.Contract;

namespace hase.DevLib.Relay
{
    public interface IRelayProxyClient<TService, TServiceProxy, TRequest, TResponse>
        where TServiceProxy : IServiceProxy<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
    }
}
