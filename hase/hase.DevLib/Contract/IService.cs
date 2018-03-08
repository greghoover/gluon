namespace hase.DevLib.Contract
{
    public interface IService<TRequest, TResponse> : IRequestResponseCommand<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
    }
}
