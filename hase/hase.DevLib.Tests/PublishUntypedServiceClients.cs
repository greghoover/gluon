using hase.AppServices.Calculator.Client;
using hase.AppServices.FileSystemQuery.Client;
using hase.DevLib.Framework.Repository.Client;
using hase.DevLib.Framework.Repository.Client.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace hase.DevLib.Tests
{
	public class PublishUntypedServiceClients
	{
		[Fact]
		public void PublishCalculator()
		{
			try
			{
				var client = new Calculator();
				var form = client.GenerateFormDefinitionFromRequestType();
				var jo = JObject.Parse(JsonConvert.SerializeObject(form));
				FormDefRepo.SaveFormDefinition(client.GetType().Name, jo);

				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}

		[Fact]
		public void PublishFileSystemQuery()
		{
			try
			{
				var client = new FileSystemQuery();
				var form = client.GenerateFormDefinitionFromRequestType();
				var jo = JObject.Parse(JsonConvert.SerializeObject(form));
				FormDefRepo.SaveFormDefinition(client.GetType().Name, jo);

				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
	}
}
