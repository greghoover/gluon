using System;
using System.Reflection;
using System.Threading.Tasks;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay;

namespace hase.DevLib.Framework.Service
{
	public abstract class ServiceBase<TRequest, TResponse> : IService<TRequest, TResponse>
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
	{
		public abstract Task<TResponse> Execute(TRequest request);

		public Task<AppResponseMessage> Execute(AppRequestMessage request)
		{
			var typedRequest = (TRequest)request;
			if (request.Fields != null)
			{
				foreach (var field in request.Fields)
				{
					var prop = typedRequest.GetType().GetProperty(field.Key);
					if (prop != null)
					{
						// todo: 06/12/18 gph. make this more generic. 
						// prob use object instead of string values in fields dictionary.
						var propType = prop.PropertyType;
						if (propType == typeof(string))
							prop.SetValue(typedRequest, field.Value);
						else if (propType == typeof(int))
							prop.SetValue(typedRequest, int.Parse(field.Value));
						else if (propType == typeof(int?))
							prop.SetValue(typedRequest, int.Parse(field.Value));
						else if (propType.IsEnum)
							prop.SetValue(typedRequest, Enum.Parse(propType, field.Value));
					}
				}
			}
			var typedResponse = this.Execute(typedRequest).Result;

			// todo: 06/12/18 gph. make this more generic. no need to double serialize.
			foreach (var prop in typedResponse.GetType().GetProperties())
			{
				switch (prop.Name)
				{
					//case "AppRequestMessage":
					case "Headers":
					case "RequestTypeName":
					case "ServiceTypeName":
					case "Fields":
						continue;
				}
				typedResponse.Fields.Add(prop.Name, prop.GetValue(typedResponse)?.ToString());
			}
			var responseTask = Task.FromResult<TResponse>(typedResponse);
			return RelayUtil.GeneralizeTask<AppResponseMessage, TResponse>(responseTask);
		}
	}
}
