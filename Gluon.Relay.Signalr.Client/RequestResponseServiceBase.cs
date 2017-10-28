using Gluon.Relay.Contracts;
using Newtonsoft.Json.Linq;
using System;

namespace Gluon.Relay.Signalr.Client
{
    public abstract class RequestResponseServiceBase<TRequest, TResponse> : IServiceType, IServiceType<TRequest, TResponse>
        where TRequest : RelayMessageBase
        where TResponse : RelayResponseBase<TRequest>
    {
        public void Execute(IRemoteMethodInvoker proxy, object request)
        {
            var json = request as JObject;
            var rqst = json.ToObject<TRequest>();
            var response = Execute(rqst);

#if DEBUG
            Console.WriteLine(response);
#endif
            proxy.InvokeAsync(CX.RelayResponseMethodName, response.CorrelationId, response).Wait();
        }

        public abstract TResponse Execute(TRequest rqst);
    }
}
