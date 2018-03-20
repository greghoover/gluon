using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay;
using System;

namespace hase.DevLib.Framework.Core
{
    public static class RelayProxyClient<TRelayProxyClient, TService, TRequest, TResponse>
        where TRelayProxyClient : IRelayProxyClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IRelayProxyClient<TService, TRequest, TResponse> CreateInstance()
        {
            return Activator.CreateInstance<TRelayProxyClient>();
        }
    }
}
