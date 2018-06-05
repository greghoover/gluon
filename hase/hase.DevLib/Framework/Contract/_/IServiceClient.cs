namespace hase.DevLib.Framework.Contract._
{
    public interface IServiceClient<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        IService<TRequest, TResponse> Service { get; }
    }
}
