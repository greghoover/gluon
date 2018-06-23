using System.Collections.Generic;

namespace hase.DevLib.Framework.Relay.Proxy
{
	public class RelayProxyConfig
	{
		public string ProxyTypeName { get; set; }
		public string ProxyConfigSection { get; set; }
		public List<string> ServiceTypeNames { get; set; }
	}
}
