using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Service;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace hase.Relays.Signalr.Client
{
	public class SignalrRelayDispatcher : RelayDispatcherBase
	{
		protected ConcurrentQueue<AppRequestMessage> Requests { get; private set; }

		public SignalrRelayDispatcher(string channelName) : base(channelName)
		{
			Requests = new ConcurrentQueue<AppRequestMessage>();
		}

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

			//_hub.On<object>("dispatch",
			_hub.On<HttpRequestMessageWrapperEx>("dispatch",
				async (req) =>
				{
					await Dispatch(req);
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
		public override Task Dispatch(HttpRequestMessageWrapperEx wrapper)
		{
			// todo: 06/05/18 gph. Revisit. Currently duplicating efforts.
			var appReq = wrapper.ToAppRequestMessage<AppRequestMessage>();
			var service = ServiceFactory2.NewLocal(appReq.ServiceClrType);
			var requestType = service.GetType().BaseType.GenericTypeArguments[0];
			var request = wrapper.ToAppRequestMessage(requestType);

			var requestId = request.Headers.MessageId;

			//Console.WriteLine($"{this.Abbr}:Enqueueing {ChannelName} request {requestId}.");
			Requests.Enqueue(request);
			Console.WriteLine($"{this.Abbr}:Enqueued {ChannelName} request {requestId}.");

			return Task.CompletedTask;
		}
		//private void Dispatch(object req)
		//{
		//    var wrapper = JsonConvert.DeserializeObject<HttpRequestMessageWrapperEx>(req.ToString());
		//    this.Dispatch(wrapper);
		//}

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
