using hase.DevLib.Framework.Contract;
using System;

namespace hase.Relays.Signalr.Client
{
	public class SignalrRelayDispatcherConfig : ConfigBase<SignalrRelayDispatcherConfig>
	{
		public override string DefaultSectionName => "SignalrRelayDispatcher";

		public Uri HubUrl { get; set; }
	}
}
