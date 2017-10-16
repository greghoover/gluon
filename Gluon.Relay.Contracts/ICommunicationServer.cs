using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface ICommunicationServer
    {
        Task<object> RelayRequestAsync(string correlationId, object request, string clientId);
        Task RelayResponseAsync(string correlationId, object response);
    }
}
