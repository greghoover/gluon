using System;

namespace hase.DevLib.Framework.Contract
{
    public class ProxyMessage
    {
        public class MessageHeaders
        {
            public string MessageId { get; set; }
            public DateTimeOffset When { get; set; }

            public MessageHeaders()
            {
                MessageId = Guid.NewGuid().ToString();
                When = DateTimeOffset.UtcNow;
            }
        }

        public MessageHeaders Headers { get; private set; }

        public ProxyMessage()
        {
            Headers = new MessageHeaders();
        }
        public ProxyMessage(object body)
        {
            Headers = new MessageHeaders();
        }
    }
}
