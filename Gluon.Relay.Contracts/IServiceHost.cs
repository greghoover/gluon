using System;

namespace Gluon.Relay.Contracts
{
    public interface IServiceHost
    {
        ICommunicationClient Hub { get; }
        string InstanceId { get; }
        bool IsInitialized { get; }
        string SubscriptionChannel { get; }

        void Initialize(string instanceId, string subscriptionId);
        IServiceType CreateServiceInstance(Type serviceType);
    }
    //public interface IServiceHost<TServiceType> where TServiceType : class
    //{
    //    void Initialize(string instanceId, string subscriptionId);
    //    TServiceType CreateServiceInstance();
    //}
}
