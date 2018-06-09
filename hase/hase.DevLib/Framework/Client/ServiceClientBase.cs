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

        public ServiceClientBase()
        {
            var serviceTypeName = this.Name + "Service";
            this.Service = ServiceFactory<TRequest, TResponse>.NewLocal(serviceTypeName);
        }
        public ServiceClientBase(Type proxyType, string proxyChannelName = null)
        {
            if (proxyChannelName == null)
            {
                //proxyChannelName = ServiceTypesUtil.GetServiceProxyName<TService>();
                proxyChannelName = this.Name + "Proxy";
            }
            this.Service = ServiceFactory<TRequest, TResponse>.NewProxied(proxyType, proxyChannelName);
        }
        //public ServiceClientBase(IService<TRequest, TResponse> serviceOrProxyInstance)
        //{
        //    this.Service = serviceOrProxyInstance;
        //}
        public InputFormDef GenerateUntypedClientDef()
        {
            var form = new InputFormDef();
            form.Name = this.Name;
            form.InputFields = new List<InputFieldDef>();

            var reqType = typeof(TRequest);
            foreach (var prop in typeof(TRequest).GetProperties())
            {
                if (prop.Name == "Headers" || prop.Name == "ServiceTypeName")
                    continue;

                var propType = prop.GetType();

                var field = new InputFieldDef();
                field.Name = prop.Name;
                field.ClrType = propType.FullName;

                if (prop.GetType().IsEnum)
                    field.Choices = Enum.GetNames(propType);

                form.InputFields.Add(field);
            }

            return form;
        }
    }
}
