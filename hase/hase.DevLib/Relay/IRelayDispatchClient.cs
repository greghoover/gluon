using hase.DevLib.Contract;

namespace hase.DevLib.Relay
{
    public interface IRelayDispatchClient<TService, TRequest, TResponse> : IRequestResponseCommand<TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class 
        where TResponse : class
    {
    }
}
