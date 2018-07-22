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

		public async Task<AppResponseMessage> Execute(AppRequestMessage request)
		{
			var typedRequest = (TRequest)request;
			if (request.Fields != null)
			{
				foreach (var field in request.Fields)
				{
					// todo: 07/22/18 gph. Check for specific error conditions.
					// e.g. if a property is not nullable and the value to assign is null, etc.
					try
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
					catch { }
				}
			}
			var typedResponse = default(TResponse);
			try
			{
				typedResponse = await this.Execute(typedRequest);
			}
			catch (Exception ex)
			{
				var txt = ex.Message;
				if (ex.InnerException != null)
					txt += Environment.NewLine + ex.InnerException.Message;
				Console.WriteLine(txt);
			}

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
			return await ContractUtil.GeneralizeTask<AppResponseMessage, TResponse>(responseTask);
		}
	}
}
