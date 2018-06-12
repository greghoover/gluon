using hase.DevLib.Framework.Contract;
using System;
using System.Reflection;

namespace hase.DevLib.Framework.Service
{
	public static class ServiceFactory<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
	{
		public static IService<TRequest, TResponse> NewLocal(string serviceTypeName)
		{
			// todo: 06/05/18 gph. Multiple convention assumptions. Need to revisit. 
			var serviceType = default(Type);
			foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.Name == serviceTypeName)
				{
					serviceType = type;
					break;
				}
			}
			return NewLocal(serviceType);
		}
		public static IService<TRequest, TResponse> NewLocal(Type serviceType)
		{
			// todo: 06/04/18 gph. Constrain serviceType to be a legit service type.
			var service = Activator.CreateInstance(serviceType);
			return (IService<TRequest, TResponse>)service;
		}
		public static IService<TRequest, TResponse> NewProxied(string proxyTypeName, string proxyChannelName)
		{
			var proxyType = Type.GetType(proxyTypeName);
			return NewProxied(proxyType, proxyChannelName);
		}
		public static IService<TRequest, TResponse> NewProxied(Type proxyType, string proxyChannelName)
		{
			// todo: 06/04/18 gph. Constrain proxyType to be a legit service proxy type.
			var proxy = Activator.CreateInstance(proxyType, proxyChannelName);
			return (IService<TRequest, TResponse>)proxy;
		}
	}
}
