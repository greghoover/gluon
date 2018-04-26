using System;

namespace hase.DevLib.Framework.Relay
{
    public static class RelayProxy<TRelayProxy, TRequest, TResponse>
        where TRelayProxy : IRelayProxy<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IRelayProxy<TRequest, TResponse> CreateInstance()
        {
            return Activator.CreateInstance<TRelayProxy>();
        }
    }
}
