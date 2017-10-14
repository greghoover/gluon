using System;

namespace Gluon.Relay.Contracts
{
    public class RelayMessageBase : IRelayMessage
    {
        public string MessageId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CorrelationId { get; set; }

        public RelayMessageBase()
        {
            this.MessageId = Guid.NewGuid().ToString();
            this.CreatedOn = DateTimeOffset.UtcNow;
            this.CorrelationId = Guid.NewGuid().ToString();
        }
    }
}
