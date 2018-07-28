using hase.AppServices.Calculator.Service;
using hase.AppServices.FileSystemQuery.Service;
using hase.DevLib.Framework.Repository.Contract;
using hase.DevLib.Framework.Repository.Service;
using hase.DevLib.Framework.Utility;
using System;
using System.IO;
using Xunit;

namespace hase.DevLib.Tests
{
	public class ServicePublishing_Remote
	{
		static string baseUri = @"http://172.27.211.17:5000";

		[Fact]
		public async void CalculatorService_RetrieveRemote()
		{
			try
			{
				var serviceName = nameof(CalculatorService);
				var folder = await ServiceRetriever.RetrieveRemote(baseUri, serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
		[Fact]
		public async void FileSystemQueryService_RetrieveRemote()
		{
			try
			{
				var serviceName = nameof(FileSystemQueryService);
				var folder = await ServiceRetriever.RetrieveRemote(baseUri, serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}

		[Fact]
		public async void CalculatorService_PublishRemote()
		{
			try
			{
				var serviceName = nameof(CalculatorService);
				var folderPath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.AppServices.Calculator\bin\Debug\netstandard2.0";

				await ServicePublisher.PublishRemote(baseUri, folderPath, serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
		[Fact]
		public async void FileSystemQueryService_PublishRemote()
		{
			try
			{
				var serviceName = nameof(FileSystemQueryService);
				var folderPath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.AppServices.FileSystemQuery\bin\Debug\netstandard2.0";

				await ServicePublisher.PublishRemote(baseUri, folderPath, serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
	}
}
