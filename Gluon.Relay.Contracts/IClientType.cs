namespace Gluon.Relay.Contracts
{
    public interface IClientType : IMessageExchangePattern
    {
        ICommunicationClient Hub { get; }
        string InstanceId { get; }
        bool IsInitialized { get; }
        string SubscriptionChannel { get; }

        void Initialize(string instanceId, string subscriptionId);

        TResponse RelayCommand<TRequest, TResponse>(TRequest request) where TRequest : RelayMessageBase where TResponse : RelayMessageBase;
        TResponse RelayQuery<TRequest, TResponse>(TRequest request) where TRequest : RelayMessageBase where TResponse : RelayMessageBase;
        void RelayEvent<TEvent>(TEvent evt) where TEvent : RelayMessageBase;
    }
}
