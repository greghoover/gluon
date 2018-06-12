using hase.DevLib.Framework.Contract;
using System;
using System.Reflection;

namespace hase.DevLib.Framework.Service
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
			var service = Activator.CreateInstance(serviceType);
			return (IService)service;
		}
		public static IService NewProxied(string proxyTypeName)
		{
			var proxyType = Type.GetType(proxyTypeName);
			return NewProxied(proxyType);
		}
		public static IService NewProxied(Type proxyType)
		{
			// todo: 06/04/18 gph. Constrain proxyType to be a legit service proxy type.
			var proxy = Activator.CreateInstance(proxyType);
			return (IService)proxy;
		}
	}
}
