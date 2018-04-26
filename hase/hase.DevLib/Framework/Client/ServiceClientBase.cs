using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using System;

namespace hase.DevLib.Framework.Client
{
    public class ServiceClientBase<TService, TRequest, TResponse> : IServiceClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public IService<TRequest, TResponse> Service { get; protected set; }

        public ServiceClientBase()
        {
            this.Service = Service<TService, TRequest, TResponse>.NewLocal();
        }
        public ServiceClientBase(Type proxyType, string proxyChannelName)
        {
            this.Service = (IService<TRequest, TResponse>)Activator.CreateInstance(proxyType, proxyChannelName);
        }
        public ServiceClientBase(IService<TRequest, TResponse> service)
        {
            this.Service = service;
        }
    }
}
