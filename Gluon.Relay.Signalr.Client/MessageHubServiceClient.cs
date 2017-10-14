using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Client
{
    public class MessageHubServiceClient<TService> : ICommunicationClient where TService : class, IServiceType
    {
        public string InstanceId { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public MessageHubServiceClient(string instanceId, string messageHubChannel, IServiceHost svcHost) : this(instanceId, new Uri(messageHubChannel), svcHost) { }
        public MessageHubServiceClient(string instanceId, Uri messageHubChannel, IServiceHost svcHost)
        {
            this.InstanceId = InstanceId;
            this.InitializeHubConnection(messageHubChannel, svcHost);
        }


        private HubConnection InitializeHubConnection(Uri messageHubChannel, IServiceHost svcHost)
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl(messageHubChannel)
                .WithConsoleLogger()
                .Build();

            // todo: refactor the client invocation method signatures
            HubConnection.On<object>(CX.WorkerMethodName, commandData =>
            {
                if (svcHost != null)
                {
                    var serviceInstance = svcHost.CreateServiceInstance(typeof(TService));
                    serviceInstance.Execute(this, commandData);
                }
            });

            HubConnection.StartAsync().Wait();
            return HubConnection;
        }

        public Task InvokeAsync(string methodName, params object[] args)
        {
            return this.HubConnection.InvokeAsync(methodName, args);
        }

        public Task<TResult> InvokeAsync<TResult>(string methodName, params object[] args)
        {
            return this.HubConnection.InvokeAsync<TResult>(methodName, args);
        }
    }
}
