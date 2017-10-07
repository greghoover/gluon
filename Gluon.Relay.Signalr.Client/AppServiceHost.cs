using Gluon.Relay.Contracts;
using System;

namespace Gluon.Relay.Signalr.Client
{
    public class AppServiceHost<TService> : IServiceHost, IServiceHost<TService> where TService : class, IServiceType
    {
        public ICommunicationClient Hub { get; private set; }
        public string InstanceId { get; private set; }
        public bool IsInitialized { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public AppServiceHost() { }
        public AppServiceHost(string instanceId, string subscriptionChannel)
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
                this.Hub = new MessageHubClient<TService>(this.InstanceId, this.SubscriptionChannel, this);
                this.IsInitialized = true;
            }
        }
        public TService CreateServiceInstance()
        {
            if (this.IsInitialized)
                return Activator.CreateInstance<TService>();
            else
                return default(TService);
        }

        IServiceType IServiceHost.CreateServiceInstance(Type serviceType)
        {
            if (this.IsInitialized)
                return (IServiceType)Activator.CreateInstance(serviceType);
            else
                return default(IServiceType);
        }
    }
}

