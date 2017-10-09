using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;

namespace Gluon.Tester.Client.Library
{
    public class RpcClient : IClientType
    {
        public ICommunicationClient Hub { get; private set; }
        public string InstanceId { get; private set; }
        public bool IsInitialized { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public RpcClient() { }
        public RpcClient(string instanceId, string subscriptionChannel)
        {
            this.Initialize(instanceId, subscriptionChannel);
        }

        public void Initialize(string instanceId, string subscriptionChannel)
        {
            if (!this.IsInitialized) // can only initialize once
            {
                this.InstanceId = InstanceId;
                this.SubscriptionChannel = subscriptionChannel;
                this.Hub = new MessageHubClient(this.InstanceId, this.SubscriptionChannel);
                this.IsInitialized = true;
            }
        }
        public RpcResponseMsg DoRequestResponse(RpcRequestMsg request)
        {
            this.Hub.InvokeAsync("DoWork", request.RequestString).Wait();
            var response = new RpcResponseMsg(request, "gitrdone");
            return response;
        }
    }
}
