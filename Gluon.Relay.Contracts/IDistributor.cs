using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface ICommunicationServer
    {
        Task PushToClient(object request, string clientId);
        //Task PushToGroup(object request, string groupId);
    }
}
