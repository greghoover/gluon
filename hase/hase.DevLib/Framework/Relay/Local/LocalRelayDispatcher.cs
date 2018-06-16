using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Local
{
	public class LocalRelayDispatcher : RelayDispatcherBase
	{
		protected static LocalRelayHub Hub { get; private set; }
		protected ConcurrentQueue<AppRequestMessage> Requests { get; private set; }

		static LocalRelayDispatcher()
		{
			if (Hub == null)
				Hub = new LocalRelayHub();
		}
		public LocalRelayDispatcher(string channelName) : base(channelName)
		{
			Requests = new ConcurrentQueue<AppRequestMessage>();
		}

		public override string Abbr => "localRelayDispatcher";

		public override Task Dispatch(HttpRequestMessageWrapperEx request)
		{
			StageRequest(request);
			return Task.CompletedTask;
		}

		public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
		{
			await Hub.StartAsync();
			await Hub.RegisterServiceDispatcherAsync(ChannelName, this).ConfigureAwait(false);
		}
		public async Task DisconnectAsync()
		{
			await Hub.UnRegisterServiceDispatcherAsync(ChannelName);
		}
		private void StageRequest(HttpRequestMessageWrapperEx wrapper)
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
		}

		public async override Task<AppRequestMessage> DeserializeRequest()
		{
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
			response.Headers.SourceChannel = this.ChannelName;
			var wrapper = response.ToTransportResponseAsync().Result;
			await Hub.DispatcherResponseAsync(ChannelName, requestId, wrapper);
		}
	}
}
