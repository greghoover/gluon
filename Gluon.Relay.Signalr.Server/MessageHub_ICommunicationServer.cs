﻿using Gluon.Relay.Contracts;
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


        public Task<object> RelayRequestAsync(string correlationId, object request, string clientId, ClientIdTypeEnum clientIdType)
        {
            var lookup = GetLookup(clientIdType, clientId);
            if (lookup != null)
            {
                var connectionId = lookup.ConnectionId;
                if (connectionId != null)
                {
                    var client = Clients.Client(connectionId);

                    if (client != null)
                    {
                        var tcs = new TaskCompletionSource<object>();
                        if (RequestResponseCache.TryAdd(correlationId, tcs))
                        {
                            // todo: refactor the client invocation method signatures
                            client.InvokeAsync(CX.WorkerMethodName, request);
                            return tcs.Task;
                        }
                    }
                }
            }

            // fallthrough
            var canceled = new TaskCompletionSource<object>();
            canceled.SetCanceled();
            return canceled.Task;
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
