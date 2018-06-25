using hase.DevLib.Framework.Contract;
using System.Collections.Generic;

namespace hase.DevLib.Framework.Relay.Dispatcher
{
	public class RelayDispatcherConfig : ConfigBase<RelayDispatcherConfig>
	{
		public override string DefaultSectionName => "ServiceDispatcher";

		public string DispatcherTypeName { get; set; }
		public string DispatcherConfigSection { get; set; }
		public List<string> ServiceTypeNames { get; set; }
	}
}
