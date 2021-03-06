﻿using Gluon.Relay.Contracts;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Gluon.Relay.Signalr.Client
{
    public class AppServiceClient : IRelayClient
    {
        public IRemoteMethodInvoker Proxy { get; private set; }
        public string InstanceId { get; private set; }
        public string SubscriptionChannel { get; private set; }

        public AppServiceClient(string instanceId, string subscriptionChannel)
        {
            this.InstanceId = instanceId;
            var qs = $"?{ClientIdTypeEnum.ClientId}={InstanceId}";
            this.SubscriptionChannel = (subscriptionChannel ?? "http://localhost:5000/messagehub") + qs;
            this.Proxy = new ServiceClientRelayProxy(this.InstanceId, this.SubscriptionChannel);
        }

        public IDictionary<string, TResponse> RelayRequestGroupResponse<TRequest, TResponse>(TRequest request, string groupId)
            where TRequest : RelayMessageBase
            where TResponse : RelayMessageBase
        {
            var result = this.Proxy.InvokeAsync<object>(CX.RelayRequestGroupMethodName, request.CorrelationId, request, groupId).Result;

            var json = result as JObject;
            var response = json.ToObject<Dictionary<string, TResponse>>();
            return response;
        }
        public TResponse RelayRequestResponse<TRequest, TResponse>(TRequest request, string clientId, ClientIdTypeEnum clientIdType)
            where TRequest : RelayMessageBase
            where TResponse : RelayMessageBase
        {
            var result = this.Proxy.InvokeAsync<object>(CX.RelayRequestMethodName, request.CorrelationId, request, clientId, clientIdType).Result;

            var json = result as JObject;
            var response = json.ToObject<TResponse>();
            return response;
        }
        public void RelayEmit<TEvent>(TEvent evt) where TEvent : RelayMessageBase
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            var hubClient = this.Proxy as ServiceClientRelayProxy;
            hubClient.HubConnection.DisposeAsync().Wait();
        }
    }
}
