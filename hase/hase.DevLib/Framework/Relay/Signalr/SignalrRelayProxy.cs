using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Signalr
{
    public class SignalrRelayProxy<TRequest, TResponse> : RelayProxyBase<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public override string Abbr => "srrpc";
        //private SignalrClientStream pipe = null;
        HubConnection _hub = null;
        private object _tmpResponse = null;

        public SignalrRelayProxy(string proxyChannelName) : base(proxyChannelName) { }

        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        {
            _hub = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/route")
                .Build();

            //_hub.On<object>("reply",
            //    (req) => {
            //        TRequest request = (TRequest)req;
            //        Console.WriteLine($"{request}");
            //    });

            try
            {
                await _hub.StartAsync();
            }
            catch (Exception ex)
            {
                var e = ex;
            }
        }

        public override void SerializeRequest(TRequest request)
        {
            //Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);
            var proxyChannel = this.ChannelName;
            var requestId = Guid.NewGuid().ToString();
            _tmpResponse = _hub.InvokeAsync<object>("ProcessProxyRequestAsync", proxyChannel, requestId, request).Result;
        }

        public override TResponse DeserializeResponse()
        {
            //return Serializer.DeserializeWithLengthPrefix<TResponse>(pipe, PrefixStyle.Base128);

            return JsonConvert.DeserializeObject<TResponse>(_tmpResponse.ToString());
        }

        public override void Disconnect()
        {
            //pipe.Dispose();
        }
    }
}
