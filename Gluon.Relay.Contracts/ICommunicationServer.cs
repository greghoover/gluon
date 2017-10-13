using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface ICommunicationServer
    {
        Task<object> RpcToClientAsync(string correlationId, object request, string clientId);
        Task PushToClientsAsync(object request, params string[] clientIds);
        //Task PushToGroupsAsync(object request, string groupIds);
    }
}
