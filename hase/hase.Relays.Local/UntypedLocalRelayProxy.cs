using hase.DevLib.Framework.Contract;

namespace hase.Relays.Local
{
	public class UntypedLocalRelayProxy : LocalRelayProxy<AppRequestMessage, AppResponseMessage>
	{
		public UntypedLocalRelayProxy(string proxyChannelName) : base(proxyChannelName) { }
	}
}
