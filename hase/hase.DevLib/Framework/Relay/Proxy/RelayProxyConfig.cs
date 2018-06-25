using hase.DevLib.Framework.Contract;
using System.Collections.Generic;

namespace hase.DevLib.Framework.Relay.Proxy
{
	public class RelayProxyConfig : ConfigBase<RelayProxyConfig>
	{
		public override string SectionName => "ServiceProxy";

		public string ProxyTypeName { get; set; }
		public string ProxyConfigSection { get; set; }
		public List<string> ServiceTypeNames { get; set; }
	}
}
