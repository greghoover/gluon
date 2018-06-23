using hase.DevLib.Framework.Contract;
using Microsoft.Extensions.Configuration;

namespace hase.Relays.Signalr.Client
{
	public class UntypedSignalrRelayProxy : SignalrRelayProxy<AppRequestMessage, AppResponseMessage>
	{
		public UntypedSignalrRelayProxy(string proxyChannelName, IConfigurationSection proxyConfig) : base(proxyChannelName, proxyConfig) { }
	}
}
