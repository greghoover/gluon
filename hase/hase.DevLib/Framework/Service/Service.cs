//using hase.DevLib.Framework.Contract;
//using System;

//namespace hase.DevLib.Framework.Service
//{
//    public static class Service<TService, TRequest, TResponse>
//        where TService : IService<TRequest, TResponse>
//        where TRequest : class
//        where TResponse : class
//    {
//        public static IService<TRequest, TResponse> NewLocal()
//        {
//            return Activator.CreateInstance<TService>();
//        }
//        public static IService<TRequest, TResponse> NewProxied<TProxy>()
//        {
//            return (IService<TRequest, TResponse>)Activator.CreateInstance<TProxy>();
//        }
//    }
//}
