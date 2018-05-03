using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Signalr
{
    public class SignalrRelayHub : Hub
    {

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private static ConcurrentDictionary<string, string> DispatcherConnections;
        private static  ConcurrentDictionary<string, string> ProxyRequests;
        private static ConcurrentDictionary<string, HttpResponseMessageWrapperEx> DispatcherResponses;

        static SignalrRelayHub()
        {
            DispatcherConnections = new ConcurrentDictionary<string, string>();
            ProxyRequests = new ConcurrentDictionary<string, string>();
            DispatcherResponses = new ConcurrentDictionary<string, HttpResponseMessageWrapperEx>();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var item = DispatcherConnections.Where(kvp => kvp.Value == Context.ConnectionId)
                 .Select(kvp => new { kvp.Key, kvp.Value })
                 .FirstOrDefault();
            
            if (item != null)
            {
                var dummy = default(string);
                DispatcherConnections.TryRemove(item.Key, out dummy);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public Task RegisterServiceDispatcherAsync(string dispatcherChannel)
        {
            var connectionId = Context.ConnectionId;
            DispatcherConnections.AddOrUpdate(dispatcherChannel, connectionId, (key, val) => { return val; });
            Console.WriteLine($"srrs:Dispatcher [{dispatcherChannel}] connection registered.");
            return Task.CompletedTask;
        }

        //public async Task<object> ProcessProxyRequestAsync(string proxyChannel, object req)
        public async Task<HttpResponseMessageWrapperEx> ProcessProxyRequestAsync(HttpRequestMessageWrapperEx request)
        {
            var proxyChannel = request.GetSourceChannel();
            var requestId = request.GetRequestId();

            var connectionId = Context.ConnectionId;
            ProxyRequests.AddOrUpdate(requestId, connectionId, (key, val) => { return val; });
            Console.WriteLine($"srrs:Proxy [{proxyChannel}] connected and request [{requestId}] received.");

            if (_cts.IsCancellationRequested)
                return null;
            var dispatcherChannel = ServiceTypesUtil.GetProxyServiceName(proxyChannel);
            Console.WriteLine($"srrs:Sending request to dispatcher [{dispatcherChannel}] [{requestId}].");
            var dispatcherConnectionId = GetDispatcherConnectionId(dispatcherChannel);
            await Clients.Client(dispatcherConnectionId).SendAsync("dispatch", request);

            if (_cts.IsCancellationRequested)
                return null;
            Console.WriteLine($"srrs:Awaiting response from dispatcher [{dispatcherChannel}].");

            HttpResponseMessageWrapperEx response = null;
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


        public Task DispatcherResponseAsync(string dispatcherChannel, string requestId, HttpResponseMessageWrapperEx response)
        {
            Console.WriteLine($"srrs:Received message from dispatcher [{dispatcherChannel}].");
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
            if (!DispatcherConnections.TryGetValue(dispatcherChannel, out dispatcherConnectionId))
                throw new ApplicationException($"No dispatcher [{dispatcherChannel}] available to handle request.");

            return dispatcherConnectionId;
        }
    }
}
