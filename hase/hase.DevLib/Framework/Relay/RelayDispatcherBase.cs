using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay
{
    public abstract class RelayDispatcherBase<TService, TRequest, TResponse> : BackgroundService, IRelayDispatcher<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        protected CancellationToken CT { get; private set; }
        public string ChannelName { get; private set; }
        public abstract string Abbr { get; }

        public abstract Task ConnectAsync(int timeoutMs, CancellationToken ct);
        public abstract Task<TRequest> DeserializeRequest();
        public abstract void SerializeResponse(string requestId, TResponse response);

        protected RelayDispatcherBase()
        {
            ChannelName = typeof(TService).Name;
        }

        private async Task SpinupConnection()
        {
            Console.WriteLine($"{this.Abbr}:{ChannelName} connecting to relay.");
            await this.ConnectAsync(timeoutMs: 5000, ct: CT);
            Console.WriteLine($"{this.Abbr}:{ChannelName} connected to relay.");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CT = stoppingToken;
            await this.SpinupConnection();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    while (!CT.IsCancellationRequested)
                    {
                        await ProcessRequest(CT);
                    }
                }
                catch (OperationCanceledException ex)
                {
                    var e = ex;
                }
                catch (Exception ex)
                {
                    var e = ex;
                }

                await this.TeardownConnection();
            }
        }
        private async Task TeardownConnection()
        {
            await Task.CompletedTask; // suppress compiler warning
            Console.WriteLine($"{this.Abbr}:{ChannelName} disconnected from relay.");
        }

        protected async virtual Task ProcessRequest(CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;
            //Console.WriteLine($"{this.Abbr}:Waiting to receive {ChannelName} request.");
            var request = await this.DeserializeRequest();
            if (request == null) return;

            if (ct.IsCancellationRequested) return;
            Console.WriteLine($"{this.Abbr}:Processing {ChannelName} request {request.Headers.MessageId}.");
            var response = await DispatchRequestAsync(request);
            if (response == null) return;
            if (ct.IsCancellationRequested) return;

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
                var e = ex; // suppress compiler warning
            }

            return response;
        }
    }
}
