using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface ICommunicationServer
    {
        Task<object> RequestToClientAsync(string correlationId, object request, string clientId);
        Task ResponseFromClientAsync(string correlationId, object response);
    }
}
