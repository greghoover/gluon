using hase.DevLib.Framework.Contract;
using System;

namespace hase.DevLib.Framework.Client
{
    public class ServiceClient<TService, TServiceProxy, TRequest, TResponse>
        where TServiceProxy : IServiceProxy<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public bool IsRemote { get; private set; }

        public ServiceClient() : this(isRemote: false)
        {
        }
        public ServiceClient(bool isRemote = false)
        {
            this.IsRemote = isRemote;
        }

        public TResponse Execute(TRequest request)
        {
            var service = ServiceFactory<TService, TServiceProxy, TRequest, TResponse>.CreateInstance(this.IsRemote);
            var response = service.Execute(request);
            return response;
        }
    }
}
