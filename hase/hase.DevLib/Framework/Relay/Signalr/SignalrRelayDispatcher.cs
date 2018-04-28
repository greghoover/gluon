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
        where TRequest : class
        where TResponse : class
    {
        private string _tmpReqId = null;
        protected ConcurrentQueue<object> Requests { get; private set; }

        public SignalrRelayDispatcher()
        {
            Requests = new ConcurrentQueue<object>();
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

            _hub.On<string, object>("dispatch",
                (reqId, req) =>
                {
                    StageRequest(reqId, req);
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
        private void StageRequest(string reqId, object req)
        {
            _tmpReqId = reqId;
            Console.WriteLine($"request [{reqId}] enqueued");
            //Console.WriteLine($"{this.Abbr}:Staging {ChannelName} request.");
            Requests.Enqueue(req);
            Console.WriteLine($"{this.Abbr}:Staged {ChannelName} request.");
        }

        public override TRequest DeserializeRequest()
        {
            //return Serializer.DeserializeWithLengthPrefix<TRequest>(pipe, PrefixStyle.Base128);

            var gotReq = false;
            object obj = null;
            while(!gotReq)
            {
                gotReq = Requests.TryDequeue(out obj);
                Task.Delay(150).Wait();
            }
            TRequest request = JsonConvert.DeserializeObject<TRequest>(obj.ToString());
            return request;
        }
        public override void SerializeResponse(TResponse response)
        {
            //Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);

            _hub.InvokeAsync("DispatcherResponseAsync", ChannelName, _tmpReqId, response);
        }
    }
}
