namespace hase.DevLib.Framework.Contract
{
    public interface IServiceProxy<TService, TRequest, TResponse> : IService<TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
    }
}
