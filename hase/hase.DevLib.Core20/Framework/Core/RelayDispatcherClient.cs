using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay;
using System;

namespace hase.DevLib.Framework.Core
{
    public static class RelayDispatcherClient<TRelayDispatcherClient, TService, TRequest, TResponse>
        where TRelayDispatcherClient : IRelayDispatcherClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IRelayDispatcherClient<TService, TRequest, TResponse> CreateInstance()
        {
            return Activator.CreateInstance<TRelayDispatcherClient>();
        }
    }
}
