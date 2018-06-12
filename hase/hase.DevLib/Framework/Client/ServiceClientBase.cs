using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace hase.DevLib.Framework.Client
{
	public abstract class ServiceClientBase<TRequest, TResponse> : IServiceClient<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage

	{
		public IService<TRequest, TResponse> Service { get; protected set; }
		public string Name => this.GetType().Name;
		public string RequestClrType => typeof(TRequest).FullName;
		public string ResponseClrType => typeof(TResponse).FullName;
		public string ServiceClrType => this.Service.GetType().FullName;

		public ServiceClientBase(string serviceTypeName = null)
		{
			this.Service = GetServiceInstance(serviceTypeName);
		}
		public ServiceClientBase(Type proxyType, string proxyChannelName = null)
		{
			proxyChannelName = proxyChannelName ?? ContractUtil.EnsureProxySuffix(this.Name);
			this.Service = GetProxyInstance(proxyType, proxyChannelName);
		}
		public ServiceClientBase(IService<TRequest, TResponse> serviceOrProxyInstance)
		{
			this.Service = serviceOrProxyInstance;
		}

		private IService<TRequest, TResponse> GetServiceInstance(string serviceTypeName = null)
		{
			serviceTypeName = serviceTypeName ?? ContractUtil.EnsureServiceSuffix(this.Name);
			return ServiceFactory<TRequest, TResponse>.NewLocal(serviceTypeName);
		}
		private IService<TRequest, TResponse> GetProxyInstance(Type proxyType, string proxyChannelName = null)
		{
			//proxyChannelName = ServiceTypesUtil.GetServiceProxyName<TService>();
			proxyChannelName = proxyChannelName ?? ContractUtil.EnsureProxySuffix(this.Name);
			return ServiceFactory<TRequest, TResponse>.NewProxied(proxyType, proxyChannelName);
		}

		public InputFormDef GenerateUntypedClientDef()
		{
			var form = new InputFormDef();
			form.Name = this.Name;
			form.RequestClrType = this.RequestClrType;
			form.ResponseClrType = this.ResponseClrType;
			form.ServiceClrType = this.ServiceClrType;
			form.InputFields = new List<InputFieldDef>();

			var reqType = typeof(TRequest);
			form.RequestClrType = reqType.FullName;
			foreach (var prop in typeof(TRequest).GetProperties())
			{
				switch (prop.Name)
				{
					case "Headers":
					case "RequestTypeName":
					case "ServiceTypeName":
					case "Fields":
						continue;
				}

				var propType = prop.PropertyType;

				var field = new InputFieldDef();
				field.Name = prop.Name;
				field.ClrType = propType.FullName;

				if (propType.IsEnum)
					field.Choices = Enum.GetNames(propType);

				form.InputFields.Add(field);
			}

			return form;
		}
	}
}
