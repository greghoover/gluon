using hase.DevLib.Framework.Contract;
using System;

namespace hase.DevLib.Framework.Relay
{
    public static class RelayDispatcher<TRelayDispatcher, TService, TRequest, TResponse>
        where TRelayDispatcher : IRelayDispatcher<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IRelayDispatcher<TService, TRequest, TResponse> CreateInstance()
        {
            return Activator.CreateInstance<TRelayDispatcher>();
        }
    }
}
