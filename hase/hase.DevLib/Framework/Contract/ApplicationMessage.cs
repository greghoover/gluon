using System;

namespace hase.DevLib.Framework.Contract
{
    public class ApplicationMessage
    {
        public class MessageHeaders
        {
            public string MessageId { get; set; }
            public DateTimeOffset? CreatedOn { get; set; }
            public string SourceChannel { get; set; }

            public MessageHeaders()
            {
                MessageId = Guid.NewGuid().ToString();
                CreatedOn = DateTimeOffset.UtcNow;
            }
        }

        public MessageHeaders Headers { get; set; }

        public ApplicationMessage()
        {
            Headers = new MessageHeaders();
        }
    }
}
