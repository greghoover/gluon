using hase.DevLib.Framework.Contract;
using System;

namespace hase.Relays.Signalr.Client
{
	public class SignalrRelayProxyConfig : ConfigBase<SignalrRelayProxyConfig>
	{
		public override string DefaultSectionName => "SignalrRelayProxy";

		public Uri HubUrl { get; set; }
	}
}
