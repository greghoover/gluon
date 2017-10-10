using Gluon.Relay.Contracts;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gluon.Relay.Signalr.Client
{
    public class MessageHubServiceClient<TService> : ICommunicationClient where TService : class, IServiceType
    {
        public static readonly string DoWorkMethodName = "DoWork";

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

                HubConnection.On<object>(DoWorkMethodName, commandData =>
                {
                    if (svcHost != null)
                    {
                        var serviceInstance = svcHost.CreateServiceInstance(typeof(TService));
                        serviceInstance.Execute(commandData);
                    }
                });

            HubConnection.StartAsync().Wait();

            return HubConnection;
        }
        public Task Send(string input)
        {
            Console.WriteLine($"Send {input}");
            return Task.CompletedTask;
        }
        public Task Send(string input, string input2)
        {
            Console.WriteLine($"Send {input}");
            return Task.CompletedTask;
        }

        public Task SendAsync(string methodName, CancellationToken cancellationToken, params object[] args)
        {
            return this.HubConnection.SendAsync(methodName, cancellationToken, args);
        }

        public Task InvokeAsync(string methodName, params object[] args)
        {
            //return this.HubConnection.InvokeAsync(methodName, returnType, cancellationToken, args);
            return this.HubConnection.InvokeAsync(methodName, args);
        }
    }
}
