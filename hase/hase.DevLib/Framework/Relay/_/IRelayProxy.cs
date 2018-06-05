using hase.DevLib.Framework.Contract._;

namespace hase.DevLib.Framework.Relay._
{
    public interface IRelayProxy<TRequest, TResponse> : IService<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        string Abbr { get; }
    }
}
