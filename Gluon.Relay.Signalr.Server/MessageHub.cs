using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    public class MessageHub : Hub, ICommunicationServer
    {
        public static ConcurrentDictionary<string, TaskCompletionSource<object>> RequestResponseCache { get; private set; }
        static MessageHub()
        {
            RequestResponseCache = new ConcurrentDictionary<string, TaskCompletionSource<object>>();
        }

        public Task<object> RpcToClientAsync(string correlationId, object request, string clientId)
        {
            correlationId = "fred";

            var tcs = new TaskCompletionSource<object>();
            RequestResponseCache.TryAdd(correlationId, tcs);

            var ids = new List<string>();
            ids.Add(Context.ConnectionId);
            var client = Clients.AllExcept(ids);
            client.InvokeAsync(CX.WorkerMethodName, request);

            return tcs.Task;
        }
        public Task RpcFromClientAsync(string correlationId, object response)
        {
            correlationId = "fred"; // temporary

            TaskCompletionSource<object> tcs;
            RequestResponseCache.TryRemove(correlationId, out tcs);
            tcs.SetResult(response);
            return Task.CompletedTask;
        }

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

        private Task PushToClientAsync(object request, string clientId)
        {
            if (clientId == null)
                return Task.CompletedTask;

            // todo: do lookup on clientId
            var client = Clients.Client(clientId);
            return this.PushToClientAsync(request, client);
        }
        private Task PushToClientAsync(object request, IClientProxy client)
        {
            if (client == null)
                return Task.CompletedTask;

            return client.InvokeAsync(CX.WorkerMethodName, request);
        }
    }
}
