using hase.DevLib.Contract;
using System;

namespace hase.DevLib.Service
{
    public static class ServiceFactory<TService, TServiceProxy, TRequest, TResponse>
        where TServiceProxy : IServiceProxy<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static IService<TRequest, TResponse> CreateInstance(bool isRemote = false)
        {
            if (isRemote)
                return Activator.CreateInstance<TServiceProxy>();
            else
                return Activator.CreateInstance<TService>();
        }
    }
}
