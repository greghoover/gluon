namespace Gluon.Relay.Contracts
{
    public interface IServiceType
    {
        object Execute(ICommunicationClient hub, object request);
    }

    public interface IServiceType<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        TResponse Execute(ICommunicationClient hub, TRequest request);
    }
}
