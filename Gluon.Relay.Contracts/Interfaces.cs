using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gluon.Relay.Contracts
{
    public interface IClientType
    {
        ICommunicationClient Hub { get; }
        string InstanceId { get; }
        bool IsInitialized { get; }
        string SubscriptionChannel { get; }

        void Initialize(string instanceId, string subscriptionId);
    }
    public interface IServiceHost
    {
        ICommunicationClient Hub { get; }
        string InstanceId { get; }
        bool IsInitialized { get; }
        string SubscriptionChannel { get; }

        void Initialize(string instanceId, string subscriptionId);
        IServiceType CreateServiceInstance(Type serviceType);
    }
    public interface IServiceHost<TServiceType> where TServiceType : class
    {
        void Initialize(string instanceId, string subscriptionId);
        TServiceType CreateServiceInstance();
    }
    public interface IServiceType
    {
        object Execute(object inputMsg);
    }
    public interface IServiceType<TInpugMsg, TOutputMsg> where TInpugMsg : class where TOutputMsg : class
    {
        TOutputMsg Execute(TInpugMsg inputMsg);
    }

    public interface ICommunicationClient : ISender, IInvoker { }

    public interface ISender
    {
        Task SendAsync(string methodName, CancellationToken cancellationToken, params object[] args);
    }
    public interface IInvoker<TReturn>
    {
        Task<TReturn> InvokeAsync(string methodName, CancellationToken cancellationToken, params object[] args);
    }
    public interface IInvoker
    {
        //Task<object> InvokeAsync(string methodName, Type returnType, CancellationToken cancellationToken, params object[] args);
        Task InvokeAsync(string methodName, params object[] args);
    }

    //public interface IEmit<TEvent>
    //{
    //    void Emit(TEvent msg);
    //}
    //public interface ISend<TSend>
    //{
    //    void Send(TSend msg);
    //}
    //public interface IReceive<TRecv>
    //{
    //    void Receive(TRecv msg);
    //}
    //public interface IRequest<TRequest, TResponse>
    //{
    //    TResponse Request(TRequest request);
    //}
    //public interface IRespond<TRequest, TResponse>
    //{
    //    TResponse Respond(TRequest request);
    //}
}
