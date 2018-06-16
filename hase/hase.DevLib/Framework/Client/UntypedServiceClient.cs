using hase.DevLib.Framework.Contract;
using System;

namespace hase.DevLib.Framework.Client
{
	public class UntypedServiceClient : ServiceClientBase<AppRequestMessage, AppResponseMessage>, IServiceClient<AppRequestMessage, AppResponseMessage>
	{
		/// <summary>
		/// Create proxied service instance.
		/// </summary>
		public UntypedServiceClient(Type proxyType, string proxyChannelName) : base(proxyType, proxyChannelName) { }
	}
}
