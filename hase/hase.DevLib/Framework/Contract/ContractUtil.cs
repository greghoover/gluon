using System;
using System.Collections.Generic;
using System.Text;

namespace hase.DevLib.Framework.Contract
{
	public static class ContractUtil
	{
		//public static string EnsureRequestSuffix(string name)
		//{
		//	if (name.ToLower().EndsWith("request"))
		//		return name;
		//	else if (name.EndsWith("Proxy"))
		//		return name.Substring(0, name.Length - 5) + "Request";
		//	else if (name.EndsWith("proxy"))
		//		return name.Substring(0, name.Length - 5) + "request";
		//	else if (name.EndsWith("Service"))
		//		return name.Substring(0, name.Length - 7) + "Request";
		//	else if (name.EndsWith("service"))
		//		return name.Substring(0, name.Length - 5) + "request";
		//	else
		//		return name + "Request";
		//}
		public static string EnsureServiceSuffix(string name)
		{
			if (name.ToLower().EndsWith("service"))
				return name;
			else if (name.EndsWith("Proxy"))
				return name.Substring(0, name.Length - 5) + "Service";
			else if (name.EndsWith("proxy"))
				return name.Substring(0, name.Length - 5) + "service";
			else
				return name + "Service";
		}
		public static string EnsureProxySuffix(string name)
		{
			if (name.ToLower().EndsWith("proxy"))
				return name;
			else if (name.EndsWith("Service"))
				return name.Substring(0, name.Length - 7) + "Proxy";
			else if (name.EndsWith("service"))
				return name.Substring(0, name.Length - 7) + "proxy";
			else
				return name + "Proxy";
		}

		//public static string GetServiceProxyName<TService>()
		//{
		//    return GetServiceProxyName(typeof(TService));
		//}
		//public static string GetServiceProxyName(Type serviceType)
		//{
		//    return GetServiceProxyName(serviceType.Name);
		//}
		//public static string GetServiceProxyName(string serviceName)
		//{
		//    if (serviceName.EndsWith("Service"))
		//        return serviceName.Substring(0, serviceName.Length - 7) + "Proxy";
		//    else if (serviceName.EndsWith("service"))
		//        return serviceName.Substring(0, serviceName.Length - 7) + "proxy";
		//    else
		//        return serviceName + "Proxy";
		//}

		public static string GetProxyServiceName(string proxyName)
		{
			if (proxyName.EndsWith("Proxy"))
				return proxyName.Substring(0, proxyName.Length - 5) + "Service";
			else if (proxyName.EndsWith("proxy"))
				return proxyName.Substring(0, proxyName.Length - 5) + "service";
			else
				return proxyName + "Service";
		}
	}
}
