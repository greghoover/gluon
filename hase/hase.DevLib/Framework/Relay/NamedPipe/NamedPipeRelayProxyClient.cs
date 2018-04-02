using ProtoBuf;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.NamedPipe
{
    public class NamedPipeRelayProxyClient<TRequest, TResponse> : RelayProxyClientBase<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public override string Abbr => "nprpc";
        private NamedPipeClientStream pipe = null;

        public NamedPipeRelayProxyClient(string proxyChannelName) : base(proxyChannelName) { }

        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        {
            pipe = new NamedPipeClientStream(".", ChannelName, PipeDirection.InOut, PipeOptions.None);
            await pipe.ConnectAsync(timeoutMs);
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
