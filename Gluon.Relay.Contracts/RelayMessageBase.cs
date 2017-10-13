using System;

namespace Gluon.Relay.Contracts
{
    public abstract class RelayMessageBase
    {
        public string CorrelationId { get; protected set; }

        public RelayMessageBase()
        {
        }
    }
}
