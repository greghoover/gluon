namespace Gluon.Relay.Contracts
{
    public interface IEvent<TEvent> : IMessageExchangePattern where TEvent : RelayMessageBase
    {
        void Emit(TEvent msg);
    }
}
