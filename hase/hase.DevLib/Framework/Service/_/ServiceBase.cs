using System.Threading.Tasks;
using hase.DevLib.Framework.Contract._;
using hase.DevLib.Framework.Relay;

namespace hase.DevLib.Framework.Service._
{
    public abstract class ServiceBase<TRequest, TResponse> : IService<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        public abstract Task<TResponse> Execute(TRequest request);

        public Task<AppResponseMessage> Execute(AppRequestMessage request)
        {
            var responseTask = this.Execute((TRequest)request);
            return RelayUtil.GeneralizeTask<AppResponseMessage, TResponse>(responseTask);
        }
    }
}
