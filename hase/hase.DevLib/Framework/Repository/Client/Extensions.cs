using hase.DevLib.Framework.Client;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Repository.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Repository.Client.Extensions
{
	public static class Extensions
	{
		public static InputFormDef GenerateFormDefinitionFromRequestType<TRequest, TResponse>(this ServiceClientBase<TRequest, TResponse> client)
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
		{
			var form = new InputFormDef();
			form.Name = client.Name;
			form.RequestClrType = client.RequestClrType;
			form.ResponseClrType = client.ResponseClrType;
			form.ServiceClrType = client.ServiceClrType;
			form.InputFields = new List<InputFieldDef>();

			var reqType = typeof(TRequest);
			form.RequestClrType = reqType.AssemblyQualifiedName;

			// add form def fields from request properties
			foreach (var prop in typeof(TRequest).GetProperties())
			{
				switch (prop.Name)
				{
					// todo: 06/16/18 gph. Un-hardcode type.
					case "Headers":
					case "RequestClrType":
					case "ServiceClrType":
					case "Fields":
						continue;
				}
				var propType = prop.PropertyType;

				var field = new InputFieldDef();
				field.Name = prop.Name;
				field.ClrType = propType.AssemblyQualifiedName;

				if (propType.IsEnum)
					field.Choices = Enum.GetNames(propType);

				form.InputFields.Add(field);
			}

			return form;
		}
		public static async Task PublishFormDefinition<TRequest, TResponse>(this ServiceClientBase<TRequest, TResponse> client, string baseUri)
		where TRequest : AppRequestMessage
		where TResponse : AppResponseMessage
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var requestUri = baseUri + $"api/formdefs/{client.Name}";

			var formDef = client.GenerateFormDefinitionFromRequestType();
			var json = JsonConvert.SerializeObject(formDef);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpClient http = new HttpClient();
			var response = await http.PutAsync(requestUri, content);

			var getDef = await http.GetAsync(requestUri);
			var getTxt = await getDef.Content.ReadAsStringAsync();
		}
	}
}
