using System;

namespace hase.DevLib.Framework.Contract
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
