using Gluon.Relay.Contracts;

namespace Gluon.Tester.Client.Library
{
    public class RpcClient : IClientType
    {
        public ICommunicationClient Hub => throw new System.NotImplementedException();

        public string InstanceId => throw new System.NotImplementedException();

        public bool IsInitialized => throw new System.NotImplementedException();

        public string SubscriptionChannel => throw new System.NotImplementedException();

        public void Initialize(string instanceId, string subscriptionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
