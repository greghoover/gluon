using hase.DevLib.Framework.Contract._;
using System;

namespace hase.DevLib.Framework.Service._
{
    public static class ServiceFactory2
    {
        public static IService NewLocal(string serviceTypeName)
        {
            var serviceType = Type.GetType(serviceTypeName);
            return NewLocal(serviceType);
        }
        public static IService NewLocal(Type serviceType)
        {
            // todo: 06/04/18 gph. Constrain serviceType to be a legit service type.
            return (IService)Activator.CreateInstance(serviceType);
        }
        public static IService NewProxied(string proxyTypeName)
        {
            var proxyType = Type.GetType(proxyTypeName);
            return NewProxied(proxyType);
        }
        public static IService NewProxied(Type proxyType)
        {
            // todo: 06/04/18 gph. Constrain proxyType to be a legit service proxy type.
            return (IService)Activator.CreateInstance(proxyType);
        }
    }
}
