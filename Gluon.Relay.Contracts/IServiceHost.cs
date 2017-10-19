using System;

namespace Gluon.Relay.Contracts
{
    public interface IServiceHost
    {
        ICommunicationClient Hub { get; }
        string InstanceId { get; }
        string SubscriptionChannel { get; }
        IServiceType CreateServiceInstance(Type serviceType);
    }
    //public interface IServiceHost<TServiceType> where TServiceType : class
    //{
    //    TServiceType CreateServiceInstance();
    //}
}
