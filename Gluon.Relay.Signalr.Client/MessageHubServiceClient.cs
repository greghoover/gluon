using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Client
{
    public class MessageHubServiceClient<TService> : IRemoteMethodInvoker where TService : class, IServiceType
    {
        public string InstanceId { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public MessageHubServiceClient(string instanceId, string messageHubChannel, IServiceHost svcHost) : this(instanceId, new Uri(messageHubChannel), svcHost) { }
        public MessageHubServiceClient(string instanceId, Uri messageHubChannel, IServiceHost svcHost)
        {
            this.InstanceId = InstanceId;

            HubConnection = (new HubConnectionBuilder() as IHubConnectionBuilder)
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
        }

        public Task InvokeAsync(string methodName, params object[] args)
        {
            // Passing InvokeAsync task back to the caller or
            // waiting on the task here both cause a lockup issue
            // on the 2nd execution attempt. So do the invoke without
            // waiting and return a completed task to avoid lockup.
            this.HubConnection.InvokeAsync(methodName, args);
            return Task.CompletedTask;
        }

        public Task<TResult> InvokeAsync<TResult>(string methodName, params object[] args)
        {
            return this.HubConnection.InvokeAsync<TResult>(methodName, args);
        }

        public void Dispose()
        {
            this.HubConnection.DisposeAsync().Wait();
        }
    }
}
