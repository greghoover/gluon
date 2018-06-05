﻿using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay
{
    public interface IRelayProxy<TRequest, TResponse> : IService<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        string Abbr { get; }
    }
}
