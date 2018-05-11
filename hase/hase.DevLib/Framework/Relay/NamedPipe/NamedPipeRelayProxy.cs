using hase.DevLib.Framework.Contract;
using ProtoBuf;
using System;
using System.IO.Pipes;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.NamedPipe
{
    public class NamedPipeRelayProxy<TRequest, TResponse> : RelayProxyBase<TRequest, TResponse>
        where TRequest : AppRequestMessage
        where TResponse : AppResponseMessage
    {
        public override string Abbr => "nprpc";
        private NamedPipeClientStream pipe = null;

        public NamedPipeRelayProxy(string proxyChannelName) : base(proxyChannelName) { }

        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        {
            try
            {
                pipe = new NamedPipeClientStream(".", ChannelName, PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.WriteThrough, TokenImpersonationLevel.None);
                await pipe.ConnectAsync(timeoutMs);
            }
            catch (Exception ex)
            {
                var e = ex; // no compiler warning please
            }
        }

        public override Task SerializeRequest(TRequest request)
        {
            Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);
            return Task.CompletedTask;
        }

        public override TResponse DeserializeResponse()
        {
            return Serializer.DeserializeWithLengthPrefix<TResponse>(pipe, PrefixStyle.Base128);
        }

        public override void DisconnectAsync()
        {
            pipe.Dispose();
        }
    }
}
