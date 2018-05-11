using System.Threading.Tasks;

namespace hase.DevLib.Framework.Contract
{
    public interface IRequestResponseCommand<TRequest, TResponse> 
        where TRequest : class 
        where TResponse : class
    {
        Task<TResponse> Execute(TRequest request);
    }
}
