namespace hase.DevLib.Framework.Contract
{
    public interface IServiceClient<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        IService<TRequest, TResponse> Service { get; }
    }
}
