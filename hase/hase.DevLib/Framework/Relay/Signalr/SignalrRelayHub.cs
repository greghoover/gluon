using hase.DevLib.Framework.Service;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Signalr
{
    public class SignalrRelayHub : Hub
    {

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private static ConcurrentDictionary<string, string> DipatcherConnections;
        private static  ConcurrentDictionary<string, string> ProxyRequests;
        private static ConcurrentDictionary<string, object> DispatcherResponses;

        static SignalrRelayHub()
        {
            DipatcherConnections = new ConcurrentDictionary<string, string>();
            ProxyRequests = new ConcurrentDictionary<string, string>();
            DispatcherResponses = new ConcurrentDictionary<string, object>();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public Task RegisterServiceDispatcherAsync(string dispatcherChannel)
        {
            var connectionId = Context.ConnectionId;
            DipatcherConnections.AddOrUpdate(dispatcherChannel, connectionId, (key, val) => { return val; });
            Console.WriteLine($"srrs:Dispatcher [{dispatcherChannel}] connection registered.");
            return Task.CompletedTask;
        }

        public async Task<object> ProcessProxyRequestAsync(string proxyChannel, string requestId, object request)
        {
            var connectionId = Context.ConnectionId;
            ProxyRequests.AddOrUpdate(requestId, connectionId, (key, val) => { return val; });
            Console.WriteLine($"srrs:Proxy [{proxyChannel}] connected and request [{requestId}] received.");

            if (_cts.IsCancellationRequested)
                return null;
            var dispatcherChannel = ServiceTypesUtil.GetProxyServiceName(proxyChannel);
            Console.WriteLine($"srrs:Sending request to [{dispatcherChannel}] dispatcher.");
            var dispatcherConnectionId = GetDispatcherConnectionId(dispatcherChannel);
            await Clients.Client(dispatcherConnectionId).SendAsync("dispatch", requestId, request);

            if (_cts.IsCancellationRequested)
                return null;
            Console.WriteLine($"srrs:Awaiting response from [{dispatcherChannel}] dispatcher.");
            object response = null;
            while (true)
            {
                if (_cts.IsCancellationRequested)
                    return null;

                Task.Delay(100).Wait();
                if (DispatcherResponses.TryRemove(requestId, out response))
                    break;
            }
            return response;
        }


        public Task DispatcherResponseAsync(string dispatcherChannel, string requestId, object response)
        {
            Console.WriteLine($"srrs:Received message from [{dispatcherChannel}] dispatcher.");
            DispatcherResponses.AddOrUpdate(requestId, response, (key, val) => { return val; });
            return Task.CompletedTask;
        }


        public async Task StartAsync()
        {
            await Task.CompletedTask; // no compiler warning please

            var ct = _cts.Token;

            Console.WriteLine($"srrs:Listening for dispatcher connections.");
            //await ListenForServiceConnectionAsync(ct); // obsolete processing model
            Console.WriteLine($"srrs:Listening for proxy connections.");
            // await proxy connections // obsolete processing model
        }
        public async Task StopAsync()
        {
            _cts.Cancel();
            await Task.Delay(1000); // time to clean up
            _cts.Dispose();
        }

        private string GetDispatcherConnectionId(string dispatcherChannel)
        {
            var dispatcherConnectionId = default(string);
            if (!DipatcherConnections.TryGetValue(dispatcherChannel, out dispatcherConnectionId))
                throw new ApplicationException($"No dispatcher [{dispatcherChannel}] available to handle request.");

            return dispatcherConnectionId;
        }
    }
}
