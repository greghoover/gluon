using hase.DevLib.Framework.Contract;
using System;
using System.Collections.Generic;

namespace hase.Relays.Signalr.Server
{
	public class SignalrRelayHubConfig : ConfigBase<SignalrRelayHubConfig>
	{
		public override string DefaultSectionName => "SignalrRelayHub";

		public List<Uri> HubUrl { get; set; }
	}
}
