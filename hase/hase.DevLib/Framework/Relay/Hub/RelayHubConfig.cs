using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay.Hub
{
	public class RelayHubConfig : ConfigBase<RelayHubConfig>
	{
		public override string SectionName => "RelayHub";

		public string HubTypeName { get; set; }
		public string HubConfigSection { get; set; }
	}
}
