using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    public class MessageHub : Hub, ICommunicationServer
    {
        public Task PushToClientsAsync(object request, params string[] clientIds)
        {
            // The real null check implementation. Bypassed temporarily.
            //if (clientIds == null)
            //    return Task.CompletedTask;

            // The temporary null check implementation. Fudge to make it send for now.
            if (clientIds == null)
            {
                var ids = new List<string>();
                ids.Add(Context.ConnectionId);
                var client = Clients.AllExcept(ids);
                this.PushToClientAsync(request, client);
                return Task.CompletedTask;
            }

            foreach (var clientId in clientIds)
            {
                this.PushToClientAsync(request, clientId);
            }
            return Task.CompletedTask;
        }

        private Task PushToClientAsync(object request, IClientProxy client)
        {
            if (client == null)
                return Task.CompletedTask;

            return client.InvokeAsync(CX.WorkerMethodName, request);
        }
        private Task PushToClientAsync(object request, string clientId)
        {
            if (clientId == null)
                return Task.CompletedTask;

            // todo: do lookup on clientId
            var client = Clients.Client(clientId);
            return this.PushToClientAsync(request, client);
        }

    }
}
