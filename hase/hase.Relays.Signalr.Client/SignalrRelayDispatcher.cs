using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.Relays.Signalr.Client
{
	public class SignalrRelayDispatcher : RelayDispatcherBase
	{
		public SignalrRelayDispatcher(string channelName) : base(channelName) { }

		public override string Abbr => "srrdc";
		//private SignalrClientStream pipe = null;
		HubConnection _hub = null;

		public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
		{
			//var handler = new HttpClientHandler
			//{
			//    ClientCertificateOptions = ClientCertificateOption.Manual,
			//    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
			//};

			_hub = new HubConnectionBuilder()
				.WithUrl($"http://{RelayUtil.RelayHostName}:5000/route")
				.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Debug))
				//.WithConsoleLogger(LogLevel.Debug)
				//.WithJsonProtocol()
				//.WithUseDefaultCredentials(true)
				//.WithTransport(TransportType.LongPolling)
				//.WithMessageHandler(h => handler)
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
			}
			finally { }
			try
			{
				await _hub.InvokeAsync("RegisterServiceDispatcherAsync", ChannelName).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				var e = ex;
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
			var wrapper = response.ToTransportResponseAsync().Result;
			await _hub.InvokeAsync("DispatcherResponseAsync", ChannelName, requestId, wrapper);
		}
	}
}
