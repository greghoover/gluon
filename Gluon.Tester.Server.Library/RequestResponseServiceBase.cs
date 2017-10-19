using Gluon.Relay.Contracts;
using Newtonsoft.Json.Linq;
using System;

namespace Gluon.Tester.Server.Library
{
    public abstract class RequestResponseServiceBase<TRequest, TResponse> : IServiceType
        where TRequest : RelayMessageBase
        where TResponse : RelayResponseBase<TRequest>
    {
        public void Execute(ICommunicationClient hub, object request)
        {
            var json = request as JObject;
            var rqst = json.ToObject<TRequest>();
            //return Execute(hub, rqst);
            var response = Execute(rqst);
            Console.WriteLine(response);
            hub.InvokeAsync(CX.RelayResponseMethodName, response.CorrelationId, response).Wait();
        }

        public abstract TResponse Execute(TRequest rqst);
    }
}
