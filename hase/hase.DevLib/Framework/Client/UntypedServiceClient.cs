using hase.DevLib.Framework.Client;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Services.Calculator.Contract;
using System;
using System.Threading.Tasks;

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
