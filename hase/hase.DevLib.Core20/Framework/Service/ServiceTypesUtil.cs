using hase.DevLib.Services;
using System;
using System.Collections.Generic;

namespace hase.DevLib.Framework.Service
{
    public static class ServiceTypesUtil
    {
        public static IEnumerable<(int Id, string Desc)> GetServices()
        {
            var serviceTypesEnum = typeof(ServiceTypesEnum);
            foreach (var vlu in Enum.GetValues(serviceTypesEnum))
            {
                var id = Convert.ToInt32(vlu);
                var desc = Enum.GetName(serviceTypesEnum, id);
                yield return (id, desc);
            }
        }

        public static string GetServiceProxyName<TService>()
        {
            return GetServiceProxyName(typeof(TService));
        }
        public static string GetServiceProxyName(Type serviceType)
        {
            return GetServiceProxyName(serviceType.Name);
        }
        public static string GetServiceProxyName(string serviceName)
        {
            if (serviceName.EndsWith("Service"))
                return serviceName.Substring(0, serviceName.Length - 7) + "Proxy";
            else if (serviceName.EndsWith("service"))
                return serviceName.Substring(0, serviceName.Length - 7) + "proxy";
            else
                return serviceName + "proxy";
        }
    }
}

