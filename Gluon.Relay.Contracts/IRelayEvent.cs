namespace Gluon.Relay.Contracts
{
    public interface IRelayEvent : IMessageExchangePattern
    {
        void RelayEvent<TEvent>(TEvent evt) where TEvent : RelayMessageBase;
    }
}
