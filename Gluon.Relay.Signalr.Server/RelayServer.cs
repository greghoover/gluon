using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    public partial class RelayServer : IRelayServer
    {
        public static ConcurrentDictionary<string, TaskCompletionSource<object>> RequestResponseCache { get; private set; }

        private static void InitRequestResponseCache()
        {
            RequestResponseCache = new ConcurrentDictionary<string, TaskCompletionSource<object>>();
        }

        public Task<object> RelayRequestGroupAsync(string groupCorrId, object request, string groupId)
        {
            var responses = new ConcurrentDictionary<string, object>();
            var tcs = new TaskCompletionSource<object>();

            // duplicate fudge for now
            var lookup = GetLookup(ClientIdTypeEnum.ClientId, groupId);
            if (lookup != null)
            {
                var connectionIds = lookup.ConnectionId;
                connectionIds += "," + connectionIds; // todo: multi-rr in different method
                if (connectionIds != null)
                {
                    foreach (var connectionId in connectionIds.Split(','))
                    {
                        var corrId = Guid.NewGuid().ToString();
                        var rqst = ((JObject)request);
                        rqst.SelectToken("correlationId").Replace(corrId);
                        var rspn = RelayRequestAsync(corrId, rqst, connectionId, ClientIdTypeEnum.ConnectionId).Result;
                        responses.TryAdd(corrId, rspn);
                    }
                }
            }
            return Task.FromResult<object>(responses);
        }
        public Task<object> RelayRequestAsync(string correlationId, object request, string clientId, ClientIdTypeEnum clientIdType)
        {
            var lookup = GetLookup(clientIdType, clientId);
            if (lookup != null)
            {
                var client = Clients.Client(lookup.ConnectionId);

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

            // fallthrough
            var canceled = new TaskCompletionSource<object>();
            canceled.SetResult(request);
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
