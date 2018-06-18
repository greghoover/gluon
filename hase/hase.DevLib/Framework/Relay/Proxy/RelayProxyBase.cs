using hase.DevLib.Framework.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Proxy
{
	public abstract class RelayProxyBase<TRequest, TResponse> : IRelayProxy<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
	{
		public string ChannelName { get; private set; }
		public abstract string Abbr { get; }

		public abstract Task ConnectAsync(int timeoutMs, CancellationToken ct);
		public abstract void DisconnectAsync();
		public abstract Task SerializeRequest(TRequest request);
		public abstract TResponse DeserializeResponse();

		private RelayProxyBase() { }
		public RelayProxyBase(string proxyChannelName)
		{
			this.ChannelName = proxyChannelName;
		}

		public async Task<TResponse> Execute(TRequest request)
		{
			Console.WriteLine($"{this.Abbr}:{this.ChannelName} connecting to relay.");
			await this.ConnectAsync(timeoutMs: 5000, ct: CancellationToken.None);
			Console.WriteLine($"{this.Abbr}:{this.ChannelName} connected.");

			Console.WriteLine($"{this.Abbr}:Sending [{this.ChannelName}] request [{request}].");
			await this.SerializeRequest(request);
			//Console.WriteLine($"{this.Abbr}:Sent [{this.ChannelName}] request.");

			//Console.WriteLine($"{this.Abbr}:Receiving [{this.ChannelName}] response.");
			TResponse response = this.DeserializeResponse();
			Console.WriteLine($"{this.Abbr}:Received [{this.ChannelName}] response [{response}].");

			//Console.WriteLine($"{this.Abbr}:{this.ChannelName} disconnecting from relay.");
			this.DisconnectAsync();
			Console.WriteLine($"{this.Abbr}:{this.ChannelName} disconnected from relay.");

			return response;
		}

		public Task<AppResponseMessage> Execute(AppRequestMessage request)
		{
			var responseTask = this.Execute((TRequest)request);
			return ContractUtil.GeneralizeTask<AppResponseMessage, TResponse>(responseTask);
		}
	}
}
