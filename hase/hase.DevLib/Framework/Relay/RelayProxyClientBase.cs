using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay
{
    public abstract class RelayProxyClientBase<TService, TRequest, TResponse> : IRelayProxyClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static readonly string ChannelName = ServiceTypesUtil.GetServiceProxyName<TService>();
        public abstract string Abbr { get; }

        public abstract Task ConnectAsync(int timeoutMs, CancellationToken ct);
        public abstract void Disconnect();
        public abstract void SerializeRequest(TRequest request);
        public abstract TResponse DeserializeResponse();

        public TResponse Execute(TRequest request)
        {
            TResponse response = null;

            Console.WriteLine($"{this.Abbr}:{ChannelName} connecting... to relay.");
            this.ConnectAsync(timeoutMs: 5000, ct: CancellationToken.None).Wait();
            Console.WriteLine($"{this.Abbr}:{ChannelName} connected.");

            Console.WriteLine($"{this.Abbr}:Sending {ChannelName} request: {request}.");
            this.SerializeRequest(request);
            //Console.WriteLine($"{this.Abbr}:Sent {ChannelName} request.");

            //Console.WriteLine($"{this.Abbr}:Receiving {ChannelName} response.");
            response = this.DeserializeResponse();
            Console.WriteLine($"{this.Abbr}:Received {ChannelName} response: {response}.");

            //Console.WriteLine($"{this.Abbr}:{ChannelName} disconnecting from relay.");
            this.Disconnect();
            Console.WriteLine($"{this.Abbr}:{ChannelName} disconnected from relay.");

            return response;
        }
    }
}
