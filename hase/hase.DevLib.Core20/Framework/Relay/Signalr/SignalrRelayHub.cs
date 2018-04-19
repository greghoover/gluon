using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication;
using ProtoBuf;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace hase.DevLib.Framework.Relay.Signalr
{
    public class SignalrRelayHub : Hub
    //public class SignalrRelayHub<TRequest, TResponse> : Hub
    //    where TRequest : class
    //    where TResponse : class
    {

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private string _dispatcherConnectionId = null;

        /// <summary>
        /// e.g. FileSystemQueryProxy
        /// </summary>
        public string ProxyChannelName { get; private set; }
        /// <summary>
        /// e.g. FileSystemQueryService
        /// </summary>
        public string ServiceChannelName { get; private set; }

        public bool ServiceIsConnected { get; set; }
        public bool ProxyIsConnected { get; set; }

        private static ConcurrentDictionary<string, string> RelayDipatcherConnections;
        private static  ConcurrentDictionary<string, string> RelayProxyConnections;
        private static ConcurrentDictionary<string, object> RelayResponseMessages;

        public SignalrRelayHub()
        {
            var proxyChannelName = "FileSystemQueryProxy";
            var serviceChannelName = "FileSystemQueryService";

            this.ProxyChannelName = proxyChannelName;
            this.ServiceChannelName = serviceChannelName;

            RelayDipatcherConnections = new ConcurrentDictionary<string, string>();
            RelayProxyConnections = new ConcurrentDictionary<string, string>();
            RelayResponseMessages = new ConcurrentDictionary<string, object>();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task RegisterServiceDispatcherAsync()
        {
            var connectionId = Context.ConnectionId;
            _dispatcherConnectionId = connectionId;
            var serviceTypeName = this.ServiceChannelName;
            RelayDipatcherConnections.AddOrUpdate(serviceTypeName, connectionId, (key, val) => { return val; });
            ServiceIsConnected = true;
            Console.WriteLine($"srrs:{this.ServiceChannelName} connected.");
            return Task.CompletedTask;
        }
        public async Task<object> ProcessProxyRequestAsync(object request)
        {
            var connectionId = Context.ConnectionId;
            var requestId = Guid.NewGuid().ToString();
            RelayProxyConnections.AddOrUpdate(requestId, connectionId, (key, val) => { return val; });
            ProxyIsConnected = true;
            Console.WriteLine($"srrs:{this.ProxyChannelName} connected.");

            //await Clients.Client(_dispatcherConnectionId).SendAsync("dispatch", request);
            await Clients.Others.SendAsync("dispatch", requestId, request);

            object response = null;
            while (true)
            {
                Task.Delay(100).Wait();
                if (RelayResponseMessages.TryRemove(requestId, out response))
                    break;
            }
            return response;
        }
        public Task ServiceResponseAsync(string requestId, object response)
        {
            RelayResponseMessages.AddOrUpdate(requestId, response, (key, val) => { return val; });
            return Task.CompletedTask;
        }


        public async Task StartAsync()
        {
            var ct = _cts.Token;

            Console.WriteLine($"srrs:Listening for {this.ServiceChannelName} connection.");
            //await ListenForServiceConnectionAsync(ct);
            Console.WriteLine($"srrs:Listening for {this.ProxyChannelName} connection.");
            //while (!ct.IsCancellationRequested)
            //{
            //    await ProcessProxyRequestAsync(ct);
            //}
        }
        public async Task StopAsync()
        {
            _cts.Cancel();
            await Task.Delay(1000); // time to clean up
            _cts.Dispose();
        }

        //private async Task ListenForServiceConnectionAsync(CancellationToken ct)
        //{
        //    Console.WriteLine($"srrs:Listening for {this.ServiceChannelName} connection.");
        //    //await _serviceChannel.WaitForConnectionAsync(ct);
        //    while (!this.ServiceIsConnected && !ct.IsCancellationRequested)
        //    {
        //        Task.Delay(150).Wait();
        //    }
        //    if (ct.IsCancellationRequested)
        //        Console.WriteLine($"srrs:Listening for {this.ServiceChannelName} connection canceled.");
        //    else
        //        Console.WriteLine($"srrs:{this.ServiceChannelName} connected.");
        //}

        //private async Task ProcessProxyRequestAsync(CancellationToken ct)
        //{
        //    try
        //    {

        //        //if (_proxyPipe != null && !_proxyPipe.IsConnected)
        //        if (!this.ProxyIsConnected)
        //        {
        //            if (ct.IsCancellationRequested) return;
        //            Console.WriteLine($"srrs:Listening for {this.ProxyChannelName} connection.");
        //            //await _proxyPipe.WaitForConnectionAsync(ct);
        //            while (!this.ProxyIsConnected && !ct.IsCancellationRequested)
        //            {
        //                Task.Delay(150).Wait();
        //            }
        //            if (ct.IsCancellationRequested)
        //                Console.WriteLine($"srrs:Listening for {this.ProxyChannelName} connection canceled.");
        //            else
        //                Console.WriteLine($"srrs:{this.ProxyChannelName} connected.");
        //        }

        //        if (ct.IsCancellationRequested) return;
        //        //Console.WriteLine($"srrs:Waiting to receive {this.ProxyChannelName} request.");
        //        TRequest request = null; // deserialize/receive request
        //        //var request = Serializer.DeserializeWithLengthPrefix<TRequest>(_proxyPipe, PrefixStyle.Base128);
        //        Console.WriteLine($"srrs:Received {this.ProxyChannelName} request: {request}.");

        //        TResponse response = null;
        //        //if (_servicePipe != null && _servicePipe.IsConnected)
        //        if (!this.ServiceIsConnected)
        //        {
        //            if (ct.IsCancellationRequested) return;
        //            Console.WriteLine($"srrs:Forwarding {this.ServiceChannelName} request: {request}.");
        //            // serialize/forward request
        //            //Serializer.SerializeWithLengthPrefix(_servicePipe, request, PrefixStyle.Base128);
        //            //Console.WriteLine($"srrs:Forwared {this.ServiceChannelName} request.");

        //            if (ct.IsCancellationRequested) return;
        //            //Console.WriteLine($"srrs:Waiting to receive {this.ServiceChannelName} response.");
        //            // deserialize/receive response
        //            //response = Serializer.DeserializeWithLengthPrefix<TResponse>(_servicePipe, PrefixStyle.Base128);
        //            Console.WriteLine($"srrs:Received {this.ServiceChannelName} response: {response}.");
        //        }


        //        if (ct.IsCancellationRequested) return;
        //        //Console.WriteLine($"srrs:Returning {this.ProxyChannelName} response.");
        //        // serialize/forward response
        //        //Serializer.SerializeWithLengthPrefix(_proxyPipe, response, PrefixStyle.Base128);
        //        Console.WriteLine($"srrs:Returned {this.ProxyChannelName} response.");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        //if (_proxyPipe != null && _proxyPipe.IsConnected)
        //        //    _proxyPipe.Disconnect();
        //    }
        //}
    }
}
