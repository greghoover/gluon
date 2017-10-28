namespace Gluon.Relay.Contracts
{
    public interface IRelayEvent : IMessageExchangePattern
    {
        void RelayEmit<TEvent>(TEvent evt) where TEvent : RelayMessageBase;
    }
}
