using hase.AppServices.Calculator.Client;
using hase.AppServices.FileSystemQuery.Client;
using System;
using Xunit;

namespace hase.DevLib.Tests
{
	public class PublishUntypedServiceClients
	{
		[Fact]
		public async void PublishCalculator()
		{
			try
			{
				var client = new Calculator();
				await client.PublishFormDefinition(@"http://172.27.211.17:5000");

				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}

		[Fact]
		public async void PublishFileSystemQuery()
		{
			try
			{
				var client = new FileSystemQuery();
				await client.PublishFormDefinition(@"http://172.27.211.17:5000");

				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
	}
}
