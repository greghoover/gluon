using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
//
using Gluon.Relay.Contracts;

namespace Gluon.Relay.Signalr.Client
{
    public class MessageHubClient : ICommunicationClient
    {
        public string InstanceId { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public MessageHubClient(string instanceId, string messageHubChannel) : this(instanceId, new Uri(messageHubChannel)) { }
        public MessageHubClient(string instanceId, Uri messageHubChannel)
        {
            this.InstanceId = InstanceId;
            this.InitializeHubConnection(messageHubChannel);
        }

        private HubConnection InitializeHubConnection(Uri messageHubChannel)
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl(messageHubChannel)
                .WithConsoleLogger()
                .Build();

            HubConnection.On<string>("Fred2", data =>
            {
                Console.WriteLine($"Received: {data}");
            });
            HubConnection.On<string>("Send", commandData =>
            {
                var worker = new Worker().DoWork("commandName", commandData);
            });
            //HubConnection.On<string, string>("Send", (commandName, commandData) =>
            //{
            //    var worker = new Worker().DoWork(commandName, commandData);
            //});
            HubConnection.On<string>("DoWork", commandData =>
            {
                var worker = new Worker().DoWork("commandName", commandData);
            });
            //HubConnection.On<string, string>("DoWork", (commandName, commandData) =>
            //{
            //    var worker = new Worker().DoWork(commandName, commandData);
            //});

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

        //public HubConnection Connect(int? timeoutSec = 10)
        //{
        //    HubConnection.StartAsync().Wait(TimeSpan.FromSeconds(10));
        //    return HubConnection;
        //}
        //public HubConnection SetupRelay<TRequest, TResponse>(string methodName, Func<TRequest, TResponse> method)
        //{
        //    _hubConnection.
        //    client.On<string>("Send", data =>
        //    {
        //        Console.WriteLine($"Received: {data}");
        //    });
        //}
    }
}
