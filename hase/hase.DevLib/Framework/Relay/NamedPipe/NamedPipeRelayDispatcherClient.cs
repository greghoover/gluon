using hase.DevLib.Framework.Contract;
using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hase.DevLib.Framework.Relay.NamedPipe
{
    public class NamedPipeRelayDispatcherClient<TService, TRequest, TResponse> : RelayDispatcherClientBase<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        private NamedPipeClientStream pipe = new NamedPipeClientStream(".", ChannelName, PipeDirection.InOut, PipeOptions.None);

        public override string Abbr => "nprdc";

        public override void Connect(int timeoutMs)
        {
            pipe.ConnectAsync(5000).Wait();
        }
        public override TRequest DeserializeRequest()
        {
            return Serializer.DeserializeWithLengthPrefix<TRequest>(pipe, PrefixStyle.Base128);
        }
        public override void SerializeResponse(TResponse response)
        {
            Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
        }
    }
}
