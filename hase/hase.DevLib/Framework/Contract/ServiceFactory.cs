using hase.DevLib.Framework.Relay.NamedPipe;
using System;

namespace hase.DevLib.Framework.Contract
{
    public static class ServiceFactory<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IService<TRequest, TResponse> CreateConfiguredInstance()
        {
            var proxy = Activator.CreateInstance<NamedPipeRelayProxyClient<TService, TRequest, TResponse>>();
            return (IService<TRequest, TResponse>)proxy;
        }
        public static IService<TRequest, TResponse> CreateLocalInstance()
        {
            return Activator.CreateInstance<TService>();
        }
    }
}
