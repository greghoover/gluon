//using hase.DevLib.Framework.Contract;
//using ProtoBuf;
//using System;
//using System.IO.Pipes;
//using System.Security.Principal;
//using System.Threading;
//using System.Threading.Tasks;

//namespace hase.DevLib.Framework.Relay.NamedPipe
//{
//    public class NamedPipeRelayDispatcher<TService, TRequest, TResponse> : RelayDispatcherBase<TService, TRequest, TResponse>
//        where TService : IService<TRequest, TResponse>
//        where TRequest : AppRequestMessage
//        where TResponse : AppResponseMessage
//    {
//        public override string Abbr => "nprdc";
//        private NamedPipeClientStream pipe = null;

//        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
//        {
//            try
//            {
//                pipe = new NamedPipeClientStream(".", ChannelName, PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
//                await pipe.ConnectAsync(timeoutMs, ct);
//            }
//            catch (Exception ex)
//            {
//                var e = ex; // no compiler warning please
//            }
//        }
//        public async override Task<TRequest> DeserializeRequest()
//        {
//            var request = Serializer.DeserializeWithLengthPrefix<TRequest>(pipe, PrefixStyle.Base128);
//            return request;
//        }
//        public override void SerializeResponse(string requestId, TResponse response)
//        {
//            Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
//        }
//    }
//}
