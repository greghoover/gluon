namespace Gluon.Relay.Contracts
{
    public interface IServiceType
    {
        void Execute(IRemoteMethodInvoker proxy, object request);
    }

    public interface IServiceType<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        TResponse Execute(TRequest request);
    }
}
