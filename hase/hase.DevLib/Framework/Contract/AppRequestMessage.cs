using System;
using System.Reflection;

namespace hase.DevLib.Framework.Contract
{
	public class AppRequestMessage : AppMessage
	{
		// todo: 06/05/18 gph. Move this into a header.
		public string RequestClrType { get; set; }
		public string ServiceClrType { get; set; }

		public AppRequestMessage()
		{
			// auto-assign for typed sub-classes
			if (this.GetType().Name != nameof(AppRequestMessage))
			{
				this.RequestClrType = this.GetType().AssemblyQualifiedName;
				this.ServiceClrType = ContractUtil.GetServiceClrTypeFromRequestType(this.GetType());
			}
		}
		public AppRequestMessage(string requestTypeName, string serviceTypeName)
		{
			this.RequestClrType = requestTypeName;
			this.ServiceClrType = serviceTypeName;
		}

		public override string ToString()
		{
			return $"RequestId[{this.Headers.MessageId}]";
		}
	}
}
