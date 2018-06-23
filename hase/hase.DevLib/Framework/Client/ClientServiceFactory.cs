using hase.DevLib.Framework.Contract;
using Microsoft.Extensions.Configuration;
using System;

namespace hase.DevLib.Framework.Client
{
	public static class ClientServiceFactory<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
	{
		public static IService<TRequest, TResponse> NewLocal(string serviceTypeAQN)
		{
			var serviceType = Type.GetType(serviceTypeAQN);
			return NewLocal(serviceType);
		}
		public static IService<TRequest, TResponse> NewLocal(Type serviceType)
		{
			// todo: 06/04/18 gph. Constrain serviceType to be a legit service type.
			var service = Activator.CreateInstance(serviceType);
			return (IService<TRequest, TResponse>)service;
		}
		public static IService<TRequest, TResponse> NewProxied(string proxyTypeAQN, IConfigurationSection proxyConfig, string proxyChannelName)
		{
			var proxyType = Type.GetType(proxyTypeAQN);
			return NewProxied(proxyType, proxyConfig, proxyChannelName);
		}
		public static IService<TRequest, TResponse> NewProxied(Type proxyType, IConfigurationSection proxyConfig, string proxyChannelName)
		{
			// todo: 06/04/18 gph. Constrain proxyType to be a legit service proxy type.
			var proxy = Activator.CreateInstance(proxyType, proxyChannelName, proxyConfig);
			return (IService<TRequest, TResponse>)proxy;
		}
	}
}
