using hase.DevLib.Framework.Contract;
using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hase.DevLib.Framework.Relay.NamedPipe
{
    public class NamedPipeRelayDispatcherClient<TService, TServiceProxy, TRequest, TResponse> : IRelayClient<TService, TServiceProxy, TRequest, TResponse>
        where TServiceProxy : IServiceProxy<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private static readonly string pipeName = typeof(TService).Name;
        private NamedPipeClientStream pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None);

        public void Run()
        {
            Console.WriteLine($"nprc:{pipeName} connecting to relay.");
            pipe.ConnectAsync(5000).Wait();
            Console.WriteLine($"nprc:{pipeName} connected to relay.");

            while (true)
            {
                ProcessRequest();
            }

        }

        private void ProcessRequest()
        {
            //Console.WriteLine($"nprc:Waiting to receive {pipeName} request.");
            var request = Serializer.DeserializeWithLengthPrefix<TRequest>(pipe, PrefixStyle.Base128);
            Console.WriteLine($"nprc:Received {pipeName} request: {request}.");

            var service = ServiceFactory<TService, TServiceProxy, TRequest, TResponse>.CreateInstance();
            TResponse response = null;
            try
            {
                response = service.Execute(request);
            }
            catch (Exception ex) { }

            Console.WriteLine($"nprc:Sending {pipeName} response: {response}.");
            Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
            //Console.WriteLine($"nprc:Sent {pipeName} response.");
        }
    }
}
