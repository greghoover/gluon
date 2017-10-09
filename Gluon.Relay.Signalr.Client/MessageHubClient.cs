using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
//
using Gluon.Relay.Contracts;

namespace Gluon.Relay.Signalr.Client
{
    public class MessageHubClient : MessageHubServiceClient<NullService>
    {
        public MessageHubClient(string instanceId, string messageHubChannel) : this(instanceId, new Uri(messageHubChannel)) { }
        public MessageHubClient(string instanceId, Uri messageHubChannel) : base(instanceId, messageHubChannel, null) { }
    }
}
