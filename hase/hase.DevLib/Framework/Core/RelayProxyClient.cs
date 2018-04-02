using hase.DevLib.Framework.Relay;
using System;

namespace hase.DevLib.Framework.Core
{
    public static class RelayProxyClient<TRelayProxyClient, TRequest, TResponse>
        where TRelayProxyClient : IRelayProxyClient<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IRelayProxyClient<TRequest, TResponse> CreateInstance()
        {
            return Activator.CreateInstance<TRelayProxyClient>();
        }
    }
}
