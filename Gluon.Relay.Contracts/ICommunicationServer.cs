using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface ICommunicationServer
    {
        Task<object> RelayRequestGroupAsync(string groupCorrId, object request, string groupId);

        Task<object> RelayRequestAsync(string correlationId, object request, string clientId, ClientIdTypeEnum clientIdType);

        Task RelayResponseAsync(string correlationId, object response);
    }
}
