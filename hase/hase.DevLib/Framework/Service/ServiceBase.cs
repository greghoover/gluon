using hase.DevLib.Framework.Contract;
using System;
using System.Threading.Tasks;

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
						var propType = prop.PropertyType;
						if (propType.IsEnum)
							prop.SetValue(typedRequest, Enum.Parse(propType, field.Value.ToString()));
						else
							prop.SetValue(typedRequest, Convert.ChangeType(field.Value, propType));
					}
				}
			}
			var typedResponse = this.Execute(typedRequest).Result;

			// todo: 06/12/18 gph. eliminate serializing multiple times.
			foreach (var prop in typedResponse.GetType().GetProperties())
			{
				switch (prop.Name)
				{
					// todo: 06/12/18 gph. Un-hardcode this.
					case "RequestTypeName": // request property
					case "ServiceTypeName": // request property
					case "AppRequestMessage": // response property
					case "Headers": // shared property
					case "Fields": // shared property
						continue;
				}
				typedResponse.Fields.Add(prop.Name, prop.GetValue(typedResponse)?.ToString());
			}
			var responseTask = Task.FromResult<TResponse>(typedResponse);
			return ContractUtil.GeneralizeTask<AppResponseMessage, TResponse>(responseTask);
		}
	}
}
