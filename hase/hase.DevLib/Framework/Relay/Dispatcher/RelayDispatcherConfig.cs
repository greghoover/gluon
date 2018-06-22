using System.Collections.Generic;

namespace hase.DevLib.Framework.Relay.Dispatcher
{
	public class RelayDispatcherConfig
	{
		public string DispatcherTypeName { get; set; }
		public string DispatcherConfigSection { get; set; }
		public List<string> ServiceTypeNames { get; set; }
	}
}
