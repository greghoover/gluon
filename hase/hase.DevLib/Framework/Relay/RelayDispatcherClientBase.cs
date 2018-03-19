using hase.DevLib.Framework.Contract;
using System;

namespace hase.DevLib.Framework.Relay
{
    public abstract class RelayDispatcherClientBase<TService, TRequest, TResponse> : IRelayDispatcherClient<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public static readonly string ChannelName = typeof(TService).Name;
        public abstract string Abbr { get; }

        public abstract void Connect(int timeoutMs);
        public abstract TRequest DeserializeRequest();
        public abstract void SerializeResponse(TResponse response);

        public virtual void Run()
        {
            Console.WriteLine($"{this.Abbr}:{ChannelName} connecting... to relay.");
            this.Connect(timeoutMs: 5000);
            Console.WriteLine($"{this.Abbr}:{ChannelName} connected to relay.");

            while (true)
            {
                ProcessRequest();
            }

        }

        protected virtual void ProcessRequest()
        {
            //Console.WriteLine($"{this.Abbr}:Waiting to receive {ChannelName} request.");
            var request = this.DeserializeRequest();
            Console.WriteLine($"{this.Abbr}:Received {ChannelName} request: {request}.");

            var response = DispatchRequest(request);

            Console.WriteLine($"{this.Abbr}:Sending {ChannelName} response: {response}.");
            this.SerializeResponse(response);
            //Console.WriteLine($"{this.Abbr}:Sent {ChannelName} response.");
        }
        protected virtual TResponse DispatchRequest(TRequest request)
        {
            var service = ServiceFactory<TService, TRequest, TResponse>.CreateLocalInstance();
            TResponse response = default(TResponse);
            try
            {
                response = service.Execute(request);
            }
            catch (Exception ex) { }

            return response;
        }
    }
}
