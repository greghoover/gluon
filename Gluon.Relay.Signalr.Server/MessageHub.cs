using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    public class MessageHub : Hub, ICommunicationServer
    {
        //public Task PushToClients(object request, params string[] clientIds)
        //{
        //    if (clientIds == null)
        //        return Task.CompletedTask;

        //    foreach (var clientId in clientIds)
        //    {
        //        //PushToClient(request, clientId);
        //        PushToClient(request, null); // just results for now
        //    }
        //    return Task.CompletedTask;
        //}
        public Task PushToClient(object request, string clientId)
        {
            IClientProxy client = null;
            if (clientId != null)
            {
                // todo: do lookup on clientId
                client = Clients.Client(clientId);
            }
            else
            {
                var ids = new List<string>();
                ids.Add(Context.ConnectionId);
                client = Clients.AllExcept(ids);
            }
            return client.InvokeAsync(CX.WorkerMethodName, request);
        }

    }
}
