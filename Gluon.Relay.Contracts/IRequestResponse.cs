using System.Collections.Generic;

namespace Gluon.Relay.Contracts
{
    public interface IRequestResponse<TRequestMsg, TResponseMsg> : IMessageExchangePattern where TRequestMsg : RelayMessageBase where TResponseMsg : RelayMessageBase
    {
        IDictionary<string, TResponseMsg> RequestGroupResponse(TRequestMsg request, string groupId);

        TResponseMsg RequestResponse(TRequestMsg request, string clientId, ClientIdTypeEnum clientIdType);
    }
}
