using hase.DevLib.Framework.Contract._;
using System;

namespace hase.DevLib.Framework.Service._
{
    public static class ServiceFactory<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        public static IService<TRequest, TResponse> NewLocal(string serviceTypeName)
        {
            var serviceType = Type.GetType(serviceTypeName);
            return NewLocal(serviceType);
        }
        public static IService<TRequest, TResponse> NewLocal(Type serviceType)
        {
            // todo: 06/04/18 gph. Constrain serviceType to be a legit service type.
            return (IService<TRequest, TResponse>)Activator.CreateInstance(serviceType);
        }
        public static IService<TRequest, TResponse> NewProxied(string proxyTypeName, string proxyChannelName)
        {
            var proxyType = Type.GetType(proxyTypeName);
            return NewProxied(proxyType, proxyChannelName);
        }
        public static IService<TRequest, TResponse> NewProxied(Type proxyType, string proxyChannelName)
        {
            // todo: 06/04/18 gph. Constrain proxyType to be a legit service proxy type.
            return (IService<TRequest, TResponse>)Activator.CreateInstance(proxyType, proxyChannelName);
        }
    }
}
