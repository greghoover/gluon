using Newtonsoft.Json.Linq;
using System;

namespace Gluon.Relay.Contracts
{
    public abstract class RequestResponseServiceBase<TRequest, TResponse> : IServiceType, IServiceType<TRequest, TResponse>
        where TRequest : RelayMessageBase
        where TResponse : RelayResponseBase<TRequest>
    {
        public void Execute(IRemoteMethodInvoker hub, object request)
        {
            var json = request as JObject;
            var rqst = json.ToObject<TRequest>();
            var response = Execute(rqst);

#if DEBUG
            Console.WriteLine(response);
#endif
            hub.InvokeAsync(CX.RelayResponseMethodName, response.CorrelationId, response).Wait();
        }

        public abstract TResponse Execute(TRequest rqst);
    }
}
