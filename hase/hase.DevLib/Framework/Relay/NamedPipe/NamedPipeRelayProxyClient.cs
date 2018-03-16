using hase.DevLib.Framework.Contract;
using ProtoBuf;
using System.IO.Pipes;

namespace hase.DevLib.Framework.Relay.NamedPipe
{
    public class NamedPipeRelayProxyClient<TService, TRequest, TResponse> : RelayProxyClientBase<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public override string Abbr => "nprpc";
        private NamedPipeClientStream pipe = null;

        public override void Connect(int timeoutMs)
        {
            pipe = new NamedPipeClientStream(".", ChannelName, PipeDirection.InOut, PipeOptions.None);
            pipe.ConnectAsync(5000).Wait();
        }

        public override void SerializeRequest(TRequest request)
        {
            Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);
        }

        public override TResponse DeserializeResponse()
        {
            return Serializer.DeserializeWithLengthPrefix<TResponse>(pipe, PrefixStyle.Base128);
        }

        public override void Disconnect()
        {
            pipe.Dispose();
        }
    }
}
