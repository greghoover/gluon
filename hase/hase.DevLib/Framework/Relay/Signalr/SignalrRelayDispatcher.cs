using hase.DevLib.Framework.Contract;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Signalr
{
    public class SignalrRelayDispatcher<TService, TRequest, TResponse> : RelayDispatcherBase<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : ProxyMessage
        where TResponse : ProxyMessage
    {
        protected ConcurrentQueue<TRequest> Requests { get; private set; }

        public SignalrRelayDispatcher()
        {
            Requests = new ConcurrentQueue<TRequest>();
        }

        public override string Abbr => "srrdc";
        //private SignalrClientStream pipe = null;
        HubConnection _hub = null;

       // public override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };

            _hub = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/route")
                //.WithConsoleLogger(LogLevel.Debug)
                //.WithJsonProtocol()
                //.WithUseDefaultCredentials(true)
                //.WithTransport(TransportType.LongPolling)
                //.WithMessageHandler(h => handler)
                .Build();

            _hub.On<object>("dispatch",
                (req) =>
                {
                    StageRequest(req);
                });

            try
            {
                await _hub.StartAsync();
                await _hub.InvokeAsync("RegisterServiceDispatcherAsync", ChannelName);
            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }
        private void StageRequest(object req)
        {
            TRequest request = JsonConvert.DeserializeObject<TRequest>(req.ToString());
            var requestId = request.Headers.MessageId;

            //Console.WriteLine($"{this.Abbr}:Enqueueing {ChannelName} request {requestId}.");
            Requests.Enqueue(request);
            Console.WriteLine($"{this.Abbr}:Enqueued {ChannelName} request {requestId}.");
        }

        public override TRequest DeserializeRequest()
        {
            //return Serializer.DeserializeWithLengthPrefix<TRequest>(pipe, PrefixStyle.Base128);

            TRequest request = null;
            while(request == null)
            {
                Requests.TryDequeue(out request);
                Task.Delay(150).Wait();
            }
            return request;
        }
        public override void SerializeResponse(string requestId, TResponse response)
        {
            //Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
            _hub.InvokeAsync("DispatcherResponseAsync", ChannelName, requestId, response);
        }
    }
}
