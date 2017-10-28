using System;

namespace Gluon.Relay.Contracts
{
    public interface IRelayClient : IHubClient, IRelayRequestResponse, IRelayEvent, IDisposable
    { }
}
