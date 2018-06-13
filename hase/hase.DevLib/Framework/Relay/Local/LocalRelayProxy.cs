using hase.DevLib.Framework.Contract;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Local
{
	public class LocalRelayProxy<TRequest, TResponse> : RelayProxyBase<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
	{
		protected static LocalRelayHub Hub { get; private set; }
		public override string Abbr => "localRelayProxy";
		private object _tmpResponse = null;

		static LocalRelayProxy()
		{
			if (Hub == null)
				Hub = new LocalRelayHub();
		}
		public LocalRelayProxy(string proxyChannelName) : base(proxyChannelName) { }

		public override Task ConnectAsync(int timeoutMs, CancellationToken ct)
		{
			return Task.CompletedTask;
		}

		public async override Task SerializeRequest(TRequest request)
		{
			request.Headers.SourceChannel = this.ChannelName;

			var wrapper = await request.ToTransportRequestAsync();

			_tmpResponse = await Hub.ProcessProxyRequestAsync(wrapper);
		}

		public override TResponse DeserializeResponse()
		{
			var wrapper = JsonConvert.DeserializeObject<HttpResponseMessageWrapperEx>(_tmpResponse.ToString());
			var response = wrapper.ToAppResponseMessage<TResponse>();
			return response;
		}

		public override void DisconnectAsync()
		{
		}
	}
}
