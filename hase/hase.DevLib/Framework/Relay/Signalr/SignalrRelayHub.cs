using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using Microsoft.AspNetCore.SignalR;
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
            UnRegisterServiceDispatcherAsync(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public Task RegisterServiceDispatcherAsync(string dispatcherChannel)
        {
            var connectionId = Context.ConnectionId;
            DispatcherConnections.AddOrUpdate(dispatcherChannel, connectionId, (key, val) => { return val; });
            Console.WriteLine($"srrs:Registered dispatcher [{dispatcherChannel}].");
            return Task.CompletedTask;
        }
        private Task UnRegisterServiceDispatcherAsync(string connectionId)
        {
            var dispatcherChannel = this.GetConnectionDispatcherChannel(connectionId);
            if (dispatcherChannel != null)
            {
                var dummy = default(string);
                if (DispatcherConnections.TryRemove(dispatcherChannel, out dummy))
                    Console.WriteLine($"srrs:UnRegistered dispatcher [{dispatcherChannel}].");

            }
            return Task.CompletedTask;
        }

        public async Task<HttpResponseMessageWrapperEx> ProcessProxyRequestAsync(HttpRequestMessageWrapperEx request)
        {
            try
            {
                var requestId = request.GetRequestId();
                var proxyChannel = request.GetSourceChannel();
                var dispatcherChannel = ContractUtil.GetProxyServiceName(proxyChannel); // simpleton routing

                Console.WriteLine($"srrs:Request [{requestId}] received on proxy channel [{proxyChannel}].");
                ProxyRequests.AddOrUpdate(requestId, Context.ConnectionId, (key, val) => { return val; });

                if (_cts.IsCancellationRequested)
                    return null;
                await ForwardRequestToDispatcher(requestId, request, dispatcherChannel);

                if (_cts.IsCancellationRequested)
                    return null;
                var response = await AwaitResponseFromDispatcher(requestId, dispatcherChannel);
                return response;
            }
            catch (Exception ex)
            {
                var txt = ex.Message;
                if (ex.InnerException != null)
                    txt += Environment.NewLine + ex.InnerException ?? string.Empty;
                Console.WriteLine(txt);

                return null;
            }
        }

        private async Task ForwardRequestToDispatcher(string requestId, HttpRequestMessageWrapperEx request, string dispatcherChannel)
        {
            Console.WriteLine($"srrs:Sending request to dispatcher [{dispatcherChannel}] [{requestId}].");
            var dispatcherConnectionId = GetDispatcherConnectionId(dispatcherChannel);
            await Clients.Client(dispatcherConnectionId).SendAsync("dispatch", request);

            return;
        }
        public Task DispatcherResponseAsync(string dispatcherChannel, string requestId, HttpResponseMessageWrapperEx response)
        {
            Console.WriteLine($"srrs:Received message from dispatcher [{dispatcherChannel}].");
            DispatcherResponses.AddOrUpdate(requestId, response, (key, val) => { return val; });
            return Task.CompletedTask;
        }
        private async Task<HttpResponseMessageWrapperEx> AwaitResponseFromDispatcher(string requestId, string dispatcherChannel)
        {
            Console.WriteLine($"srrs:Awaiting response from dispatcher [{dispatcherChannel}].");
            HttpResponseMessageWrapperEx response = null;
            while (true)
            {
                if (_cts.IsCancellationRequested)
                    return null;

                await Task.Delay(150);
                if (DispatcherResponses.TryRemove(requestId, out response))
                    break;
            }
            return response;
        }

        public async Task StartAsync()
        {
            await Task.Delay(1); // hush compile warning
            Console.WriteLine($"srrs:Listening for dispatcher connections.");
            Console.WriteLine($"srrs:Listening for proxy connections.");
        }
        public async Task StopAsync()
        {
            try
            {
                await Task.Delay(1); // hush compile warning
                if (_cts != null)
                {
                    _cts.Cancel();
                    Task.Delay(1000).Wait(); // time to clean up
                }
            }
            finally
            {
                if (_cts != null)
                    _cts.Dispose();
            }
        }

        private string GetDispatcherConnectionId(string dispatcherChannel)
        {
            var dispatcherConnectionId = default(string);
            if (!DispatcherConnections.TryGetValue(dispatcherChannel, out dispatcherConnectionId))
                throw new ApplicationException($"Could not retrieve connection id for unregistered dispatcher channel [{dispatcherChannel}] .");

            return dispatcherConnectionId;
        }
        private string GetConnectionDispatcherChannel(string connectionId)
        {
            return DispatcherConnections.Where(kvp => kvp.Value == connectionId)
                 .Select(kvp => new { kvp.Key, kvp.Value })
                 .FirstOrDefault()?.Key;
        }
    }
}
