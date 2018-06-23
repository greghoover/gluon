using hase.DevLib.Framework.Contract;
using Microsoft.Extensions.Configuration;
using System;

namespace hase.DevLib.Framework.Client
{
	public class ServiceClient : ServiceClientBase<AppRequestMessage, AppResponseMessage>, IServiceClient<AppRequestMessage, AppResponseMessage>
	{
		/// <summary>
		/// Create proxied service instance.
		/// </summary>
		public ServiceClient(Type proxyType, IConfigurationSection proxyConfig, string proxyChannelName) : base(proxyType, proxyConfig, proxyChannelName) { }
	}
}
