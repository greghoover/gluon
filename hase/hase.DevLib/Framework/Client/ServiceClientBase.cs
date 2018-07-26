using hase.DevLib.Framework.Contract;
using Microsoft.Extensions.Configuration;
using System;

namespace hase.DevLib.Framework.Client
{
	public abstract class ServiceClientBase<TRequest, TResponse> : IServiceClient<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage

	{
		public IService<TRequest, TResponse> Service { get; protected set; }
		public string Name => this.GetType().Name;
		public string RequestClrType => typeof(TRequest).AssemblyQualifiedName;
		public string ResponseClrType => typeof(TResponse).AssemblyQualifiedName;
		public string ServiceClrType => this.Service.GetType().AssemblyQualifiedName;

		public ServiceClientBase(string serviceTypeName = null)
		{
			this.Service = GetServiceInstance(serviceTypeName);
		}
		public ServiceClientBase(Type proxyType, IConfigurationSection proxyConfig, string proxyChannelName = null)
		{
			proxyChannelName = proxyChannelName ?? ContractUtil.EnsureProxySuffix(this.Name);
			this.Service = GetProxyInstance(proxyType, proxyConfig, proxyChannelName);
		}
		public ServiceClientBase(IService<TRequest, TResponse> serviceOrProxyInstance)
		{
			this.Service = serviceOrProxyInstance;
		}

		private IService<TRequest, TResponse> GetServiceInstance(string serviceTypeName = null)
		{
			serviceTypeName = serviceTypeName ?? ContractUtil.GetServiceClrTypeFromClientType(this.GetType());
			return ClientServiceFactory<TRequest, TResponse>.NewLocal(serviceTypeName);
		}
		private IService<TRequest, TResponse> GetProxyInstance(Type proxyType, IConfigurationSection proxyConfig, string proxyChannelName = null)
		{
			proxyChannelName = proxyChannelName ?? ContractUtil.EnsureProxySuffix(this.Name);
			return ClientServiceFactory<TRequest, TResponse>.NewProxied(proxyType, proxyConfig, proxyChannelName);
		}
	}
}
