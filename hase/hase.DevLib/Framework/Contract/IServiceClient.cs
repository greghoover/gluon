namespace hase.DevLib.Framework.Contract
{
    public interface IServiceClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        IService<TRequest, TResponse> Service { get; }
    }
}
