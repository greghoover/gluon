using System.Collections.Generic;

namespace Gluon.Relay.Contracts
{
    public interface IClientType : IMessageExchangePattern
    {
        ICommunicationClient Hub { get; }
        string InstanceId { get; }
        string SubscriptionChannel { get; }

        Dictionary<string, TResponse> RelayRequestGroupResponse<TRequest, TResponse>(TRequest request, string groupId) where TRequest : RelayMessageBase where TResponse : RelayMessageBase;
        TResponse RelayRequestResponse<TRequest, TResponse>(TRequest request, string clientId, ClientIdTypeEnum clientIdType) where TRequest : RelayMessageBase where TResponse : RelayMessageBase;
        void RelayEvent<TEvent>(TEvent evt) where TEvent : RelayMessageBase;
    }
}
