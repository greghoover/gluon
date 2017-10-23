using Gluon.Relay.Contracts;
using System.Collections.Concurrent;

namespace Gluon.Relay.Signalr.Server
{
    public partial class MessageHub
    {
        public static ConcurrentDictionary<string, LogonMsg> ClientLookup { get; private set; }

        private static void InitClientLookup()
        {
            ClientLookup = new ConcurrentDictionary<string, LogonMsg>();
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
    }
}
