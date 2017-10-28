namespace Gluon.Relay.Contracts
{
    public interface IHubClient
    {
        IRemoteMethodInvoker Hub { get; }
        string InstanceId { get; }
        string SubscriptionChannel { get; }
    }
}
