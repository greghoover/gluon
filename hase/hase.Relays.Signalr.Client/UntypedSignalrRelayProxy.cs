using hase.DevLib.Framework.Contract;

namespace hase.Relays.Signalr.Client
{
	public class UntypedSignalrRelayProxy : SignalrRelayProxy<AppRequestMessage, AppResponseMessage>
	{
		public UntypedSignalrRelayProxy(string proxyChannelName) : base(proxyChannelName) { }
	}
}
