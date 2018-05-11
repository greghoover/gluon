using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay
{
    public abstract class RelayDispatcherBase<TService, TRequest, TResponse> : IRelayDispatcher<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        protected CancellationTokenSource _cts { get; private set; }
        public string ChannelName { get; private set; }
        public abstract string Abbr { get; }

        public abstract Task ConnectAsync(int timeoutMs, CancellationToken ct);
        public abstract Task<TRequest> DeserializeRequest();
        public abstract void SerializeResponse(string requestId, TResponse response);

        protected RelayDispatcherBase()
        {
            _cts = new CancellationTokenSource();
            ChannelName = typeof(TService).Name;
        }

    public async Task StartAsync()
        {
            var ct = _cts.Token;

            Console.WriteLine($"{this.Abbr}:{ChannelName} connecting to relay.");
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
            await Task.CompletedTask; // no compiler warning please

            if (ct.IsCancellationRequested) return;
            //Console.WriteLine($"{this.Abbr}:Waiting to receive {ChannelName} request.");
            var request = await this.DeserializeRequest();
            Console.WriteLine($"{this.Abbr}:Processing {ChannelName} request {request.Headers.MessageId}.");

            if (ct.IsCancellationRequested) return;
            var response = await DispatchRequestAsync(request);

            if (ct.IsCancellationRequested) return;
            Console.WriteLine($"{this.Abbr}:Sending {ChannelName} response {response.Headers.MessageId} for request {request.Headers.MessageId}.");
            this.SerializeResponse(request.Headers.MessageId, response);
            //Console.WriteLine($"{this.Abbr}:Sent {ChannelName} response.");
        }
        protected async virtual Task<TResponse> DispatchRequestAsync(TRequest request)
        {
            var service = Service<TService, TRequest, TResponse>.NewLocal();
            TResponse response = default(TResponse);
            try
            {
                response = await service.Execute(request);

                if (response.AppRequestMessage == null)
                    response.AppRequestMessage = request;
            }
            catch (Exception ex)
            {
                var e = ex; // no compiler warning please
            }

            return response;
        }
    }
}
