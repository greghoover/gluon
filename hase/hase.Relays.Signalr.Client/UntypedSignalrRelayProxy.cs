using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay.Signalr
{
	public class UntypedSignalrRelayProxy : SignalrRelayProxy<AppRequestMessage, AppResponseMessage>
	{
		public UntypedSignalrRelayProxy(string proxyChannelName) : base(proxyChannelName) { }
	}
}
