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
        public static IService<TRequest, TResponse> NewProxied<TProxy>()
        {
            return (IService<TRequest, TResponse>)Activator.CreateInstance<TProxy>();
        }
    }
}
