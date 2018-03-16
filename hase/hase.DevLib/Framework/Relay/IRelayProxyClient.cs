﻿using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay
{
    public interface IRelayProxyClient<TService, TRequest, TResponse> : IServiceProxy<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
    }
}