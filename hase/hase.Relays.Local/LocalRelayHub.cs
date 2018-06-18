using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Contract;
using hase.DevLib.Framework.Relay.Dispatcher;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace hase.Relays.Local
{
	public class LocalRelayHub
	{
		private string Abbr => "localRelayHub";
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();

		private static ConcurrentDictionary<string, RelayDispatcherBase> Dispatchers;
		//private static ConcurrentDictionary<string, string> DispatcherConnections;
		private static  ConcurrentDictionary<string, string> ProxyRequests;
		private static ConcurrentDictionary<string, HttpResponseMessageWrapperEx> DispatcherResponses;

		static LocalRelayHub()
		{
			Dispatchers = new ConcurrentDictionary<string, RelayDispatcherBase>();
			//DispatcherConnections = new ConcurrentDictionary<string, string>();
			ProxyRequests = new ConcurrentDictionary<string, string>();
			DispatcherResponses = new ConcurrentDictionary<string, HttpResponseMessageWrapperEx>();
		}

		public Task RegisterServiceDispatcherAsync(string dispatcherChannel, RelayDispatcherBase dispatcher)
		{
			//var connectionId = Context.ConnectionId;
			Dispatchers.AddOrUpdate(dispatcherChannel, dispatcher, (key, val) => { return val; });
			//DispatcherConnections.AddOrUpdate(dispatcherChannel, connectionId, (key, val) => { return val; });
			Console.WriteLine($"{Abbr}:Registered dispatcher [{dispatcherChannel}].");
			return Task.CompletedTask;
		}

		//private Task UnRegisterServiceDispatcherAsync(string connectionId)
		public Task UnRegisterServiceDispatcherAsync(string dispatcherChannel)
		{
			//var dispatcherChannel = this.GetConnectionDispatcherChannel(connectionId);
			if (dispatcherChannel != null)
			{
				//var dummy = default(string);
				var dummy = default(RelayDispatcherBase);
				//if (DispatcherConnections.TryRemove(dispatcherChannel, out dummy))
				if (Dispatchers.TryRemove(dispatcherChannel, out dummy))
					Console.WriteLine($"{Abbr}:UnRegistered dispatcher [{dispatcherChannel}].");

			}
			return Task.CompletedTask;
		}

		public async Task<HttpResponseMessageWrapperEx> ProcessProxyRequestAsync(HttpRequestMessageWrapperEx request)
		{
			try
			{
				var requestId = request.GetRequestId();
				var proxyChannel = request.GetSourceChannel();
				var dispatcherChannel = ContractUtil.GetProxyServiceName(proxyChannel); // simpleton routing

				Console.WriteLine($"{Abbr}:Request [{requestId}] received on proxy channel [{proxyChannel}].");
				var connectionId = request.GetCustomRequestHeader(LocalRelayUtil.ConnectionIdHeaderName);
				ProxyRequests.AddOrUpdate(requestId, connectionId, (key, val) => { return val; });

				if (_cts.IsCancellationRequested)
					return null;
				await ForwardRequestToDispatcher(requestId, request, dispatcherChannel);

				if (_cts.IsCancellationRequested)
					return null;
				var response = await AwaitResponseFromDispatcher(requestId, dispatcherChannel);
				return response;
			}
			catch (Exception ex)
			{
				var txt = ex.Message;
				if (ex.InnerException != null)
					txt += Environment.NewLine + ex.InnerException ?? string.Empty;
				Console.WriteLine(txt);

				return null;
			}
		}

		private async Task ForwardRequestToDispatcher(string requestId, HttpRequestMessageWrapperEx request, string dispatcherChannel)
		{
			Console.WriteLine($"{Abbr}:Sending request to dispatcher [{dispatcherChannel}] [{requestId}].");
			//var dispatcherConnectionId = GetDispatcherConnectionId(dispatcherChannel);
			var dispatcher = GetDispatcher(dispatcherChannel);
			//await Clients.Client(dispatcherConnectionId).SendAsync("dispatch", request);
			await dispatcher.StageRequest(request);

			return;
		}
		public Task DispatcherResponseAsync(string dispatcherChannel, string requestId, HttpResponseMessageWrapperEx response)
		{
			Console.WriteLine($"{Abbr}:Received message from dispatcher [{dispatcherChannel}].");
			DispatcherResponses.AddOrUpdate(requestId, response, (key, val) => { return val; });
			return Task.CompletedTask;
		}
		private async Task<HttpResponseMessageWrapperEx> AwaitResponseFromDispatcher(string requestId, string dispatcherChannel)
		{
			Console.WriteLine($"{Abbr}:Awaiting response from dispatcher [{dispatcherChannel}].");
			HttpResponseMessageWrapperEx response = null;
			while (true)
			{
				if (_cts.IsCancellationRequested)
					return null;

				await Task.Delay(150);
				if (DispatcherResponses.TryRemove(requestId, out response))
					break;
			}
			return response;
		}

		public async Task StartAsync()
		{
			await Task.Delay(1); // quiet compiler warning
			Console.WriteLine($"{Abbr}:Listening for dispatcher connections.");
			Console.WriteLine($"{Abbr}:Listening for proxy connections.");
		}
		public async Task StopAsync()
		{
			try
			{
				await Task.Delay(1); // hush compile warning
				if (_cts != null)
				{
					_cts.Cancel();
					Task.Delay(1000).Wait(); // time to clean up
				}
			}
			finally
			{
				if (_cts != null)
					_cts.Dispose();
			}
		}

		private RelayDispatcherBase GetDispatcher(string dispatcherChannel)
		{
			var dispatcher = default(RelayDispatcherBase);
			if (!Dispatchers.TryGetValue(dispatcherChannel, out dispatcher))
				throw new ApplicationException($"Could not get Dispatcher object for unregistered dispatcher channel [{dispatcherChannel}] .");

			return dispatcher;
		}
	}
}
