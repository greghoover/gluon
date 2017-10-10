using System;

namespace Gluon.Relay.Signalr.Client
{
    public class MessageHubClient : MessageHubServiceClient<NullService>
    {
        public MessageHubClient(string instanceId, string messageHubChannel) : this(instanceId, new Uri(messageHubChannel)) { }
        public MessageHubClient(string instanceId, Uri messageHubChannel) : base(instanceId, messageHubChannel, null) { }
    }
}
