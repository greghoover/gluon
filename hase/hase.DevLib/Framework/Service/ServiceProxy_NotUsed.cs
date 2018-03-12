//using hase.DevLib.Framework.Contract;
//using hase.DevLib.Framework.Relay.NamedPipe;

//namespace hase.DevLib.Framework.Service
//{
//    public class ServiceProxy<TService, TRequest, TResponse> : IServiceProxy<TService, TRequest, TResponse>
//        where TService : IService<TRequest, TResponse>
//        where TRequest : class
//        where TResponse : class
//    {
//        public TResponse Execute(TRequest request)
//        {
//            var proxy = RelayProxyClientFactory<NamedPipeRelayProxyClient<TService, TRequest, TResponse>, TService, TRequest, TResponse>.CreateInstance();
//            return proxy.Execute(request);
//        }
//    }
//}
