using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Contract;
using hase.DevLib.Framework.Relay.Dispatcher;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.Relays.Signalr.Client
{
	public class SignalrRelayDispatcher : RelayDispatcherBase
	{
		public SignalrRelayDispatcher(string channelName, IConfigurationSection dispatcherConfig) : base(channelName)
		{
			// todo: 06/26/18. Generalize into base class.
			if (dispatcherConfig is SignalrRelayDispatcherConfig)
				this.Config = (SignalrRelayDispatcherConfig)dispatcherConfig;
			else
				this.Config = dispatcherConfig.Get<SignalrRelayDispatcherConfig>();
		}

		public override string Abbr => "signalrDispatcher";
		//private SignalrClientStream pipe = null;
		HubConnection _hub = null;
		public SignalrRelayDispatcherConfig Config { get; private set; }

		public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
		{
			_hub = new HubConnectionBuilder()
				.WithUrl(this.Config.HubUrl)
				.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Debug))
				.Build();

			_hub.On<HttpRequestMessageWrapperEx>("dispatch",
				async (request) =>
				{
					await StageRequest(request);
				});

			try
			{
				await _hub.StartAsync();
			}
			catch (Exception ex)
			{
				var e = ex;
				throw;
			}
			finally { }
			try
			{
				await _hub.InvokeAsync("RegisterServiceDispatcherAsync", ChannelName).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				var e = ex;
				throw;
			}
			finally { }
		}

		public async override Task<AppRequestMessage> DeserializeRequest()
		{
			//return Serializer.DeserializeWithLengthPrefix<AppRequestMessage>(pipe, PrefixStyle.Base128);
			AppRequestMessage request = null;
			while(!this.CT.IsCancellationRequested && request == null)
			{
				Requests.TryDequeue(out request);
				await Task.Delay(150);
			}
			return request;
		}
		public async override void SerializeResponse(string requestId, AppResponseMessage response)
		{
			//Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
			response.Headers.SourceChannel = this.ChannelName;
			var wrapper = await response.ToTransportResponseAsync();
			await _hub.InvokeAsync("DispatcherResponseAsync", ChannelName, requestId, wrapper);
		}
	}
}
