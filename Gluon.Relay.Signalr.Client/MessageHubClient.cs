﻿using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
//
using Gluon.Relay.Contracts;

namespace Gluon.Relay.Signalr.Client
{
    public class MessageHubClient : ICommunicationClient
    {
        public static readonly string WorkerClassName = "Worker";
        public static readonly string DoWorkMethodName = "DoWork";
        public string InstanceId { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public MessageHubClient(string instanceId, string messageHubChannel, Assembly asm) : this(instanceId, new Uri(messageHubChannel), asm) { }
        public MessageHubClient(string instanceId, Uri messageHubChannel, Assembly asm)
        {
            this.InstanceId = InstanceId;
            this.InitializeHubConnection(messageHubChannel, asm);
        }

        private HubConnection InitializeHubConnection(Uri messageHubChannel, Assembly asm)
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl(messageHubChannel)
                .WithConsoleLogger()
                .Build();

            //HubConnection.On<string>("Send", commandData =>
            //{
            //    var worker = new Worker().DoWork("commandName", commandData);
            //});
            ////HubConnection.On<string, string>("Send", (commandName, commandData) =>
            ////{
            ////    var worker = new Worker().DoWork(commandName, commandData);
            ////});
            HubConnection.On<string>(DoWorkMethodName, commandData =>
            {
                var typeName = asm.GetName().Name + "." + WorkerClassName;
                var type = asm.GetType(typeName, throwOnError: false, ignoreCase: true);
                var meth = type.GetMethod(DoWorkMethodName);
                var instance = Activator.CreateInstance(type, this);
                var obj = meth.Invoke(instance, new object[] { DoWorkMethodName, commandData });
                var str = obj.ToString();
                //var worker = new Worker().DoWork("commandName", commandData);
            });
            //HubConnection.On<string, string>(DoWorkMethodName, (commandName, commandData) =>
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
    }
}
