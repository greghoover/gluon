using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.NamedPipe;
using System;

namespace hase.DevLib.Framework.Core
{
    public static class Service<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IService<TRequest, TResponse> NewLocal()
        {
            return Activator.CreateInstance<TService>();
        }
        //public static IService<TRequest, TResponse> CreateNamedPipeRelayInstance()
        //{
        //    var proxy = Activator.CreateInstance<NamedPipeRelayProxyClient<TService, TRequest, TResponse>>();
        //    return (IService<TRequest, TResponse>)proxy;
        //}
        public static IService<TRequest, TResponse> NewProxied<TProxy>()
        {
            IService<TRequest, TResponse> proxy = null;
            if (typeof(TProxy) == typeof(NamedPipeRelayProxyClient<TService, TRequest, TResponse>))
                proxy = Activator.CreateInstance<NamedPipeRelayProxyClient<TService, TRequest, TResponse>>();

            return proxy;
        }
    }
}
