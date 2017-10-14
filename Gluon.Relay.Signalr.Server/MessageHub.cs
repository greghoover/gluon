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

        public Task<object> RequestToClientAsync(string correlationId, object request, string clientId)
        {
            var tcs = new TaskCompletionSource<object>();
            RequestResponseCache.TryAdd(correlationId, tcs);

            var ids = new List<string>();
            ids.Add(Context.ConnectionId);
            var client = Clients.AllExcept(ids);
            // todo: refactor the client invocation method signatures
            client.InvokeAsync(CX.WorkerMethodName, request);

            return tcs.Task;
        }
        public Task ResponseFromClientAsync(string correlationId, object response)
        {
            TaskCompletionSource<object> tcs;
            RequestResponseCache.TryRemove(correlationId, out tcs);
            tcs.SetResult(response);
            return Task.CompletedTask;
        }
    }
}
