using System;

namespace Gluon.Relay.Contracts
{
    public interface IServiceHost : IHubProxy
    {
        IServiceType CreateServiceInstance(Type serviceType);
    }
    //public interface IServiceHost<TServiceType> where TServiceType : class
    //{
    //    TServiceType CreateServiceInstance();
    //}
}
