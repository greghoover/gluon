namespace Gluon.Relay.Contracts
{
    public interface IClientType : IMessageExchangePattern
    {
        ICommunicationClient Hub { get; }
        string InstanceId { get; }
        string SubscriptionChannel { get; }

        TResponse RelayRequestResponse<TRequest, TResponse>(TRequest request) where TRequest : RelayMessageBase where TResponse : RelayMessageBase;
        void RelayEvent<TEvent>(TEvent evt) where TEvent : RelayMessageBase;
    }
}
