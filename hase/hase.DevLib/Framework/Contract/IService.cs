using System.Threading.Tasks;

namespace hase.DevLib.Framework.Contract
{
    public interface IService
    {
        Task<AppResponseMessage> Execute(AppRequestMessage request);
    }

    public interface IService<TRequest, TResponse> : IService
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        Task<TResponse> Execute(TRequest request);
    }
}
