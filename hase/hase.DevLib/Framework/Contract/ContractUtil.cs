using System;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Contract
{
	public static class ContractUtil
	{
		public static string GetServiceClrTypeFromClientType(Type clientType)
		{
			var clientName = clientType.Name;
			var serviceName = EnsureServiceSuffix(clientType.Name);
			var clientAQN = clientType.AssemblyQualifiedName;
			var serviceAQN = clientAQN.Replace($".Client.{clientName},", $".Service.{serviceName},");
			return serviceAQN;
		}
		public static string GetServiceNameFromClientType(Type clientType)
		{
			var clientName = clientType.Name;
			var serviceName = EnsureServiceSuffix(clientType.Name);
			return serviceName;
		}
		public static Type GetServiceTypeFromClientType(Type clientType)
		{
			var clientName = clientType.Name;
			var serviceName = EnsureServiceSuffix(clientType.Name);
			var clientAQN = clientType.AssemblyQualifiedName;
			var serviceAQN = clientAQN.Replace($".Client.{clientName},", $".Service.{serviceName},");
			var serviceType = Type.GetType(serviceAQN);
			return serviceType;
		}
		public static string GetServiceClrTypeFromRequestType(Type requestType)
		{
			var requestName = requestType.Name;
			var serviceName = EnsureServiceSuffix(requestType.Name);
			var requestAQN = requestType.AssemblyQualifiedName;
			var serviceAQN = requestAQN.Replace($".Contract.{requestName},", $".Service.{serviceName},");
			return serviceAQN;
		}
		public static string EnsureServiceSuffix(string name)
		{
			if (name.ToLower().EndsWith("service"))
				return name;
			else if (name.EndsWith("Request"))
				return name.Substring(0, name.Length - 7) + "Service";
			else if (name.EndsWith("request"))
				return name.Substring(0, name.Length - 7) + "service";
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

		public static async Task<TBase> GeneralizeTask<TBase, TDerived>(Task<TDerived> task)
			where TDerived : TBase
		{
			return (TBase)await task;
		}
	}
}
