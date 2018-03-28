using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay
{
    public abstract class RelayDispatcherClientBase<TService, TRequest, TResponse> : IRelayDispatcherClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        protected readonly CancellationTokenSource _cts = new CancellationTokenSource();
        public static readonly string ChannelName = typeof(TService).Name;
        public abstract string Abbr { get; }

        public abstract Task ConnectAsync(int timeoutMs, CancellationToken ct);
        public abstract TRequest DeserializeRequest();
        public abstract void SerializeResponse(TResponse response);

        public async Task StartAsync()
        {
            var ct = _cts.Token;

            Console.WriteLine($"{this.Abbr}:{ChannelName} connecting... to relay.");
            await this.ConnectAsync(timeoutMs: 5000, ct: ct);
            Console.WriteLine($"{this.Abbr}:{ChannelName} connected to relay.");

            while (!ct.IsCancellationRequested)
            {
                await ProcessRequest(ct);
            }
        }
        public async Task StopAsync()
        {
            _cts.Cancel();
            await Task.Delay(1000); // time to clean up
            _cts.Dispose();
        }

        protected async virtual Task ProcessRequest(CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;
            //Console.WriteLine($"{this.Abbr}:Waiting to receive {ChannelName} request.");
            var request = this.DeserializeRequest();
            Console.WriteLine($"{this.Abbr}:Received {ChannelName} request: {request}.");

            if (ct.IsCancellationRequested) return;
            var response = DispatchRequest(request);

            if (ct.IsCancellationRequested) return;
            Console.WriteLine($"{this.Abbr}:Sending {ChannelName} response: {response}.");
            this.SerializeResponse(response);
            //Console.WriteLine($"{this.Abbr}:Sent {ChannelName} response.");
        }
        protected virtual TResponse DispatchRequest(TRequest request)
        {
            var service = Service<TService, TRequest, TResponse>.NewLocal();
            TResponse response = default(TResponse);
            try
            {
                response = service.Execute(request);
            }
            catch (Exception ex) { }

            return response;
        }
    }
}
