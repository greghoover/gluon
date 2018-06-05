//using hase.DevLib.Framework.Contract;
//using hase.DevLib.Framework.Service;
//using System;

//namespace hase.DevLib.Framework.Client
//{
//    // Experimental
//    public static class ClientUtil
//    {
//        public static T WithLocal<T>(this T client)
//            where T : class, IServiceClient<IService<AppRequestMessage, AppResponseMessage>, AppRequestMessage, AppResponseMessage>
//        {
//            //var local = Service.Service<>
//            //var local = Activator.CreateInstance<TService, IService<AppRequestMessage, AppResponseMessage>>();
//            //client.Service = default(IService<AppRequestMessage, AppResponseMessage>);
//            return client as T;
//        }
//    }
//}
