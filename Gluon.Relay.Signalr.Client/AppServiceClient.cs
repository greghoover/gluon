using Gluon.Relay.Contracts;

namespace Gluon.Relay.Signalr.Client
{
    public class AppServiceClient<TService> : IClientType where TService : class, IServiceType
    {
        public ICommunicationClient Hub { get; private set; }
        public string InstanceId { get; private set; }
        public bool IsInitialized { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public AppServiceClient() { }
        public AppServiceClient(string instanceId, string subscriptionChannel)
        {
            this.Initialize(instanceId, subscriptionChannel);
        }

        /// <summary>
        /// Does nothing if already initialized.
        /// </summary>
        public void Initialize(string instanceId, string subscriptionChannel)
        {
            if (!this.IsInitialized) // Can only initialize once.
            {
                this.InstanceId = instanceId ?? typeof(TService).Name;
                this.SubscriptionChannel = subscriptionChannel ?? "http://localhost:5000/messagehub";
                var svcHost = new AppServiceHost<TService>(this.InstanceId, this.SubscriptionChannel);
                //this.Hub = new MessageHubClient<TService>(this.InstanceId, this.SubscriptionChannel, svcHost);
                this.Hub = svcHost.Hub;
                this.IsInitialized = true;
            }
        }
    }
}
