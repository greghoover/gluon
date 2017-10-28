using System;

namespace Gluon.Relay.Contracts
{
    public interface IRelayClient : IHubProxy, IRelayRequestResponse, IRelayEvent, IDisposable
    { }
}
