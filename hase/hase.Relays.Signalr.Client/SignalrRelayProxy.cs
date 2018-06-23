using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Contract;
using hase.DevLib.Framework.Relay.Proxy;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.Relays.Signalr.Client
{
	public class SignalrRelayProxy<TRequest, TResponse> : RelayProxyBase<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
	{
		public override string Abbr => "signalrProxy";
		//private SignalrClientStream pipe = null;
		HubConnection _hub = null;
		private object _tmpResponse = null;
		public SignalrRelayProxyConfig Config { get; private set; }

		public SignalrRelayProxy(string proxyChannelName, IConfigurationSection proxyConfig) : base(proxyChannelName)
		{
			this.Config = proxyConfig.Get<SignalrRelayProxyConfig>();
		}

		public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
		{
			try
			{
				_hub = new HubConnectionBuilder()
					.WithUrl(this.Config.HubUrl)
					.Build();

				await _hub.StartAsync();
			}
			catch (Exception ex)
			{
				var e = ex;
				if (_hub != null)
					await _hub.DisposeAsync();
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
