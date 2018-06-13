using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay.Local
{
	public class UntypedLocalRelayProxy : LocalRelayProxy<AppRequestMessage, AppResponseMessage>
	{
		public UntypedLocalRelayProxy(string proxyChannelName) : base(proxyChannelName) { }
	}
}
