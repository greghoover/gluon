using System;

namespace Gluon.Relay.Signalr.Client
{
    internal class ServiceClientRelayProxy : ServiceHostRelayProxy<NullService>
    {
        public ServiceClientRelayProxy(string instanceId, string messageHubChannel) : this(instanceId, new Uri(messageHubChannel)) { }
        public ServiceClientRelayProxy(string instanceId, Uri messageHubChannel) : base(instanceId, messageHubChannel, svcHost: null) { }
    }
}
