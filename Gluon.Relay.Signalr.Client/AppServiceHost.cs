using Gluon.Relay.Contracts;
using System;

namespace Gluon.Relay.Signalr.Client
{
    public class AppServiceHost<TService> : IDisposable, IServiceHost where TService : class, IServiceType
    {
        public IRemoteMethodInvoker Hub { get; private set; }
        public string InstanceId { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public AppServiceHost() { }
        public AppServiceHost(string instanceId, string subscriptionChannel)
        {
            this.InstanceId = instanceId ?? typeof(TService).Name;
            var qs = $"?{ClientIdTypeEnum.ClientId}={InstanceId}";
            this.SubscriptionChannel = (subscriptionChannel ?? "http://localhost:5000/messagehub") + qs;
            this.Hub = new MessageHubServiceClient<TService>(this.InstanceId, this.SubscriptionChannel, this);
        }

        IServiceType IServiceHost.CreateServiceInstance(Type serviceType)
        {
            return (IServiceType)Activator.CreateInstance(serviceType);
        }

        public void Dispose()
        {
            this.Hub.Dispose();
        }
    }
}

