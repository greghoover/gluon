﻿using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay
{
	public abstract class RelayDispatcherBase : BackgroundService, IRelayDispatcher
	{
		protected ConcurrentQueue<AppRequestMessage> Requests { get; private set; }
		protected CancellationToken CT { get; private set; }
		public string ChannelName { get; private set; }
		public abstract string Abbr { get; }

		public abstract Task ConnectAsync(int timeoutMs, CancellationToken ct);
		public abstract void SerializeResponse(string requestId, AppResponseMessage response);

		protected RelayDispatcherBase(string channelName)
		{
			this.Requests = new ConcurrentQueue<AppRequestMessage>();
			this.CT = new CancellationToken();
			this.ChannelName = channelName; // e.g. some variation of the service name
		}

		private async Task SpinupConnection()
		{
			Console.WriteLine($"{this.Abbr}:{ChannelName} dispatcher connecting to relay.");
			await this.ConnectAsync(timeoutMs: 100000, ct: CT);
			Console.WriteLine($"{this.Abbr}:{ChannelName} dispatcher connected to relay.");
		}
		public async Task ExecutePublicAsync(CancellationToken stoppingToken)
		{
			await ExecuteAsync(stoppingToken);
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			CT = stoppingToken;
			await this.SpinupConnection();

			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					while (!CT.IsCancellationRequested)
					{
						await ProcessRequest(CT);
					}
				}
				catch (OperationCanceledException ex)
				{
					var e = ex;
				}
				catch (Exception ex)
				{
					var e = ex;
				}

				await this.TeardownConnection();
			}
		}
		private async Task TeardownConnection()
		{
			await Task.CompletedTask; // suppress compiler warning
			Console.WriteLine($"{this.Abbr}:{ChannelName} disconnected from relay.");
		}

		public virtual Task StageRequest(HttpRequestMessageWrapperEx wrapper)
		{
			// todo: 06/18/18 gph. Put the request clr type into a header
			// so don't have to convert from wrapper twice.
			var appReq = wrapper.ToAppRequestMessage<AppRequestMessage>();
			var requestType = Type.GetType(appReq.RequestClrType);
			var request = wrapper.ToAppRequestMessage(requestType);

			var requestId = request.Headers.MessageId;

			//Console.WriteLine($"{this.Abbr}:Enqueueing {ChannelName} request {requestId}.");
			Requests.Enqueue(request);
			Console.WriteLine($"{this.Abbr}:Enqueued {ChannelName} request {requestId}.");

			return Task.CompletedTask;
		}
		public virtual async Task<AppRequestMessage> DeserializeRequest()
		{
			AppRequestMessage request = null;
			while (!this.CT.IsCancellationRequested && request == null)
			{
				Requests.TryDequeue(out request);
				await Task.Delay(150);
			}
			return request;
		}
		protected async virtual Task ProcessRequest(CancellationToken ct)
		{
			if (ct.IsCancellationRequested) return;
			//Console.WriteLine($"{this.Abbr}:Waiting to receive {ChannelName} request.");
			var request = await this.DeserializeRequest();
			if (request == null) return;

			if (ct.IsCancellationRequested) return;
			Console.WriteLine($"{this.Abbr}:Processing {ChannelName} request {request.Headers.MessageId}.");
			var response = await DispatchRequestAsync(request);
			if (response == null) return;
			if (ct.IsCancellationRequested) return;

			if (ct.IsCancellationRequested) return;
			Console.WriteLine($"{this.Abbr}:Sending {ChannelName} response {response.Headers.MessageId} for request {request.Headers.MessageId}.");
			this.SerializeResponse(request.Headers.MessageId, response);
			//Console.WriteLine($"{this.Abbr}:Sent {ChannelName} response.");
		}
		protected async virtual Task<AppResponseMessage> DispatchRequestAsync(AppRequestMessage request)
		{
			var service = DispatcherServiceFactory.NewLocal(request.ServiceClrType);
			AppResponseMessage response = default(AppResponseMessage);
			try
			{
				response = await service.Execute(request);

				if (response.AppRequestMessage == null)
					response.AppRequestMessage = request;
			}
			catch (Exception ex)
			{
				var e = ex; // suppress compiler warning
			}

			return response;
		}
	}
}
