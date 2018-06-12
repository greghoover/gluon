using System;
using System.Collections.Generic;

namespace hase.DevLib.Framework.Contract
{
	public class AppMessage
	{
		public class MessageHeaders
		{
			public string MessageId { get; set; }
			public DateTimeOffset? CreatedOn { get; set; }
			public string SourceChannel { get; set; }
			public Dictionary<string, string> Custom;

			public MessageHeaders()
			{
				MessageId = Guid.NewGuid().ToString();
				CreatedOn = DateTimeOffset.UtcNow;
				Custom = new Dictionary<string, string>();
			}
		}

		public MessageHeaders Headers { get; set; }
		public IDictionary<string, object> Fields { get; set; }

		public AppMessage()
		{
			Headers = new MessageHeaders();
			Fields = new Dictionary<string, object>();
		}
	}
}
