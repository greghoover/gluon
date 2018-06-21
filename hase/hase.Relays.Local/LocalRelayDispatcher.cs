using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Contract;
using hase.DevLib.Framework.Relay.Dispatcher;
using System.Threading;
using System.Threading.Tasks;

namespace hase.Relays.Local
{
	public class LocalRelayDispatcher : RelayDispatcherBase
	{
		protected static LocalRelayHub Hub { get; private set; }

		static LocalRelayDispatcher()
		{
			if (Hub == null)
				Hub = new LocalRelayHub();
		}
		public LocalRelayDispatcher(string channelName) : base(channelName) { }

		public override string Abbr => "localDispatcher";

		public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
		{
			await Hub.StartAsync();
			await Hub.RegisterServiceDispatcherAsync(ChannelName, this).ConfigureAwait(false);
		}
		public async Task DisconnectAsync()
		{
			await Hub.UnRegisterServiceDispatcherAsync(ChannelName);
		}

		public async override void SerializeResponse(string requestId, AppResponseMessage response)
		{
			response.Headers.SourceChannel = this.ChannelName;
			var wrapper = await response.ToTransportResponseAsync();
			await Hub.DispatcherResponseAsync(ChannelName, requestId, wrapper);
		}
	}
}
