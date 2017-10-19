using Gluon.Relay.Contracts;
using Newtonsoft.Json.Linq;

namespace Gluon.Relay.Signalr.Client
{
    public class AppServiceClient : IClientType
    {
        public ICommunicationClient Hub { get; private set; }
        public MessageHubClient HubClient { get { return Hub as MessageHubClient; } }
        public string InstanceId { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public AppServiceClient(string instanceId, string subscriptionChannel)
        {
            this.InstanceId = instanceId;
            var qs = $"?{ClientSpecEnum.ClientId}={InstanceId}";
            this.SubscriptionChannel = (subscriptionChannel ?? "http://localhost:5000/messagehub") + qs;
            this.Hub = new MessageHubClient(this.InstanceId, this.SubscriptionChannel);
        }

        public TResponse RelayRequestResponse<TRequest, TResponse>(TRequest request, string clientId)
            where TRequest : RelayMessageBase
            where TResponse : RelayMessageBase
        {
            var result = this.Hub.InvokeAsync<object>(CX.RelayRequestMethodName, request.CorrelationId, request, clientId).Result;

            var json = result as JObject;
            var response = json.ToObject<TResponse>();
            return response;
        }
        public void RelayEvent<TEvent>(TEvent evt) where TEvent : RelayMessageBase
        {
            throw new System.NotImplementedException();
        }
    }
}
