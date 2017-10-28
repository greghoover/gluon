namespace Gluon.Relay.Contracts
{
    public interface IHubProxy
    {
        IRemoteMethodInvoker Proxy { get; }
        string InstanceId { get; }
        string SubscriptionChannel { get; }
    }
}
