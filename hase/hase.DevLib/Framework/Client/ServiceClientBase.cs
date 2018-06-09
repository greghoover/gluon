using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using System;

namespace hase.DevLib.Framework.Client
{
    public abstract class ServiceClientBase<TRequest, TResponse> : IServiceClient<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage

    {
        public IService<TRequest, TResponse> Service { get; protected set; }

        public ServiceClientBase()
        {
            var serviceTypeName = this.GetType().Name + "Service";
            this.Service = ServiceFactory<TRequest, TResponse>.NewLocal(serviceTypeName);
        }
        public ServiceClientBase(Type proxyType, string proxyChannelName = null)
        {
            if (proxyChannelName == null)
            {
                //proxyChannelName = ServiceTypesUtil.GetServiceProxyName<TService>();
                proxyChannelName = this.GetType().Name + "Proxy";
            }
            this.Service = ServiceFactory<TRequest, TResponse>.NewProxied(proxyType, proxyChannelName);
        }
        //public ServiceClientBase(IService<TRequest, TResponse> serviceOrProxyInstance)
        //{
        //    this.Service = serviceOrProxyInstance;
        //}
    }
}
