using System.Collections.Generic;

namespace Gluon.Relay.Contracts
{
    public interface IRelayRequestResponse : IMessageExchangePattern
    {
        IDictionary<string, TResponse> RelayRequestGroupResponse<TRequest, TResponse>(TRequest request, string groupId) where TRequest : RelayMessageBase where TResponse : RelayMessageBase;

        TResponse RelayRequestResponse<TRequest, TResponse>(TRequest request, string clientId, ClientIdTypeEnum clientIdType) where TRequest : RelayMessageBase where TResponse : RelayMessageBase;
    }
}
