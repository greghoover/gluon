using System;

namespace Gluon.Relay.Contracts
{
    public interface IRelayMessage
    {
        string MessageId { get; set; }
        DateTimeOffset CreatedOn { get; set; }
        string CorrelationId { get; set; }
    }
}
