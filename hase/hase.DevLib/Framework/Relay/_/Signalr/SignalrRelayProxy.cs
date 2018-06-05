﻿using hase.DevLib.Framework.Contract._;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay._.Signalr
{
    public class SignalrRelayProxy<TRequest, TResponse> : RelayProxyBase<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        public override string Abbr => "srrpc";
        //private SignalrClientStream pipe = null;
        HubConnection _hub = null;
        private object _tmpResponse = null;

        public SignalrRelayProxy(string proxyChannelName) : base(proxyChannelName) { }

        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        {
            foreach (var addy in RelayUtil.RelayIPs)
            {
                if (addy.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    continue;
                try
                {
                    _hub = new HubConnectionBuilder()
                        .WithUrl($"http://{addy.ToString()}:5000/route")
                        .Build();

                    await _hub.StartAsync();
                    break;
                }
                catch (Exception ex)
                {
                    var e = ex;
                    if (_hub != null)
                        await _hub.DisposeAsync();
                }
            }
        }

        public async override Task SerializeRequest(TRequest request)
        {
            //Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);

            request.Headers.SourceChannel = this.ChannelName;

            var wrapper = await request.ToTransportRequestAsync();

            _tmpResponse = await _hub.InvokeAsync<object>("ProcessProxyRequestAsync", wrapper);
        }

        public override TResponse DeserializeResponse()
        {
            //return Serializer.DeserializeWithLengthPrefix<TResponse>(pipe, PrefixStyle.Base128);

            //return JsonConvert.DeserializeObject<TResponse>(_tmpResponse.ToString());
            var wrapper = JsonConvert.DeserializeObject<HttpResponseMessageWrapperEx>(_tmpResponse.ToString());
            var response = wrapper.ToAppResponseMessage<TResponse>();
            return response;
        }

        public async override void DisconnectAsync()
        {
            //pipe.Dispose();
            await _hub.DisposeAsync();
        }
    }
}
