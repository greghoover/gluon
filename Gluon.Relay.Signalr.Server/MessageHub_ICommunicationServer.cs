using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    public partial class MessageHub : ICommunicationServer
    {
        public static ConcurrentDictionary<string, TaskCompletionSource<object>> RequestResponseCache { get; private set; }

        private static void InitRequestResponseCache()
        {
            RequestResponseCache = new ConcurrentDictionary<string, TaskCompletionSource<object>>();
        }


        public Task<object> RelayRequestAsync(string correlationId, object request, string clientId)
        {
            var tcs = new TaskCompletionSource<object>();
            var tf = RequestResponseCache.TryAdd(correlationId, tcs);

            IClientProxy client = null;
            if (clientId == null)
            {
                var ids = new List<string>();
                ids.Add(Context.ConnectionId);
                client = Clients.AllExcept(ids);
            }
            else
            {
                var lookup = GetLookup(ClientSpecEnum.ClientId, clientId);
                if (lookup != null)
                {
                    var connectionId = lookup.ConnectionId;
                    if (connectionId != null)
                    {
                        client = Clients.Client(connectionId);
                    }
                }
            }

            // todo: refactor the client invocation method signatures
            if (client != null)
                client.InvokeAsync(CX.WorkerMethodName, request);

            return tcs.Task;
        }
        public Task RelayResponseAsync(string correlationId, object response)
        {
            TaskCompletionSource<object> tcs;
            RequestResponseCache.TryRemove(correlationId, out tcs);
            if (tcs != null)
                tcs.SetResult(response);
            return Task.CompletedTask;
        }
    }
}
