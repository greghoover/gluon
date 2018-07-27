using System.Collections.Generic;

namespace hase.DevLib.Framework.Repository.Client
{
	public class InputFormDef
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string RequestClrType { get; set; }
		public string ResponseClrType { get; set; }
		public string ServiceClrType { get; set; }
		public string NavigationTitle { get; set; }
		public string ContentTitle { get; set; }
		public List<InputFieldDef> InputFields { get; set; }
	}
}
