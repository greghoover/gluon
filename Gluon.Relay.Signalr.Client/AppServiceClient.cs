using Gluon.Relay.Contracts;
using Newtonsoft.Json.Linq;

namespace Gluon.Relay.Signalr.Client
{
    public abstract class AppServiceClient : IClientType
    {
        public ICommunicationClient Hub { get; private set; }
        public MessageHubClient HubClient { get { return Hub as MessageHubClient; } }
        public string InstanceId { get; private set; }
        public bool IsInitialized { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public AppServiceClient() { }
        public AppServiceClient(string instanceId, string subscriptionChannel)
        {
            this.Initialize(instanceId, subscriptionChannel);
        }

        /// <summary>
        /// Does nothing if already initialized.
        /// </summary>
        public void Initialize(string instanceId, string subscriptionChannel)
        {
            if (!this.IsInitialized) // Can only initialize once.
            {
                this.InstanceId = instanceId;
                this.SubscriptionChannel = subscriptionChannel ?? "http://localhost:5000/messagehub";
                this.Hub = new MessageHubClient(this.InstanceId, this.SubscriptionChannel);
                this.IsInitialized = true;
            }
        }

        public TResponse RelayRequestResponse<TRequest, TResponse>(TRequest request)
            where TRequest : RelayMessageBase
            where TResponse : RelayMessageBase
        {
            var result = this.Hub.InvokeAsync<object>(CX.RelayRequestMethodName, request.CorrelationId, request, null).Result;

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
