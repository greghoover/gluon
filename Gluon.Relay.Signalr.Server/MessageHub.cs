using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Sockets.Http.Features;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Server
{
    //[Authorize]
    public class MessageHub : Hub, ICommunicationServer
    {
        public static ConcurrentDictionary<string, TaskCompletionSource<object>> RequestResponseCache { get; private set; }
        public static ConcurrentDictionary<string, LogonMsg> ClientLookup { get; private set; }

        static MessageHub()
        {
            RequestResponseCache = new ConcurrentDictionary<string, TaskCompletionSource<object>>();
            ClientLookup = new ConcurrentDictionary<string, LogonMsg>();
        }

        public override Task OnConnectedAsync()
        {
            var msg = new LogonMsg
            {
                ConnectionId = this.Context.ConnectionId,
                ClientId = this.GetHttpContextItem<string>(ClientSpecEnum.ClientId.ToString()),
                UserId = this.GetHttpContextItem<string>(ClientSpecEnum.UserId.ToString()),
            };

            this.Logon(msg);

            return base.OnConnectedAsync();
        }
        private T GetHttpContextItem<T>(string key)
        {
            var httpContext = Context.Connection.Features.Get<IHttpContextFeature>().HttpContext;
            object vlu;
            if (httpContext.Items.TryGetValue(key, out vlu))
                return (T)vlu;
            else
                return default(T);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var msg = new LogoffMsg
            {
                ClientIdentifier = new ClientIdentifier
                {
                    ClientIdentifierType = ClientSpecEnum.ConnectionId,
                    ClientIdentifierValue = Context.ConnectionId,
                }
            };

            this.Logoff(msg);

            return base.OnDisconnectedAsync(exception);
        }

        private void Logon(LogonMsg msg)
        {
            if (msg == null || msg.ConnectionId == null)
                return; // Task.CompletedTask;

            UpdateLookup(ClientSpecEnum.ConnectionId, msg.ConnectionId, msg);
            UpdateLookup(ClientSpecEnum.ClientId, msg.ClientId, msg);
            UpdateLookup(ClientSpecEnum.UserId, msg.UserId, msg);

            return; // Task.CompletedTask;
        }
        private void UpdateLookup(ClientSpecEnum? clientIdType, string clientIdValue, LogonMsg msg)
        {
            if (clientIdType == null || clientIdValue == null || msg == null)
                return;

            var key = $"{clientIdType.ToString()}:{clientIdValue}";
            ClientLookup.AddOrUpdate(key, msg, (k, old) => msg);
        }

        private void Logoff(LogoffMsg msg)
        {
            if (msg == null)
                return; // Task.CompletedTask;

            var client = msg.ClientIdentifier;
            if (client == null || client.ClientIdentifierType == null || client.ClientIdentifierValue == null)
                return; // Task.CompletedTask;

            var lom = RemoveLookup(client);
            RemoveLookup(ClientSpecEnum.ClientId, lom.ClientId);
            RemoveLookup(ClientSpecEnum.ConnectionId, lom.ConnectionId);
            RemoveLookup(ClientSpecEnum.UserId, lom.UserId);

            return; // Task.CompletedTask;
        }
        private LogonMsg RemoveLookup(ClientIdentifier client)
        {
            if (client == null)
                return default(LogonMsg);

            var clientIdType = client.ClientIdentifierType;
            var clientIdValue = client.ClientIdentifierValue;

            return this.RemoveLookup(clientIdType, clientIdValue);
        }
        private LogonMsg RemoveLookup(ClientSpecEnum? clientIdType, string clientIdValue)
        {
            if (clientIdType == null || clientIdValue == null)
                return default(LogonMsg);

            LogonMsg msg;
            var key = $"{clientIdType.ToString()}:{clientIdValue}";
            if (ClientLookup.TryRemove(key, out msg))
                return msg;
            else
                return default(LogonMsg);
        }
        private LogonMsg GetLookup(ClientSpecEnum? clientIdType, string clientIdValue)
        {
            if (clientIdType == null || clientIdValue == null)
                return default(LogonMsg);

            LogonMsg msg;
            var key = $"{clientIdType.ToString()}:{clientIdValue}";
            if (ClientLookup.TryGetValue(key, out msg))
                return msg;
            else
                return default(LogonMsg);
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
            tcs.SetResult(response);
            return Task.CompletedTask;
        }
    }
}
