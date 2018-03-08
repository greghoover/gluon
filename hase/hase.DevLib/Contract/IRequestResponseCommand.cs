namespace hase.DevLib.Contract
{
    public interface IRequestResponseCommand<TRequest, TResponse> 
        where TRequest : class 
        where TResponse : class
    {
        TResponse Execute(TRequest request);
    }
}
