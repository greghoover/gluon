using hase.DevLib.Framework.Contract;
using System;
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
			request.Headers.Custom.Add(LocalRelayUtil.ConnectionIdHeaderName, Guid.NewGuid().ToString());

			var wrapper = await request.ToTransportRequestAsync();

			_tmpResponse = await Hub.ProcessProxyRequestAsync(wrapper);
		}

		public override TResponse DeserializeResponse()
		{
			try
			{
				var wrapper = (HttpResponseMessageWrapperEx)_tmpResponse;
				var response = wrapper.ToAppResponseMessage<TResponse>();
				return response;
			}
			catch (Exception ex)
			{
				var txt = ex.Message;
				if (ex.InnerException != null)
					txt += Environment.NewLine + ex.InnerException.Message;
				throw;
			}
		}

		public override void DisconnectAsync()
		{
		}
	}
}
