using hase.AppServices.Calculator.Service;
using hase.AppServices.FileSystemQuery.Service;
using hase.DevLib.Framework.Repository.Service;
using System;
using Xunit;

namespace hase.DevLib.Tests
{
	public class ServicePublishing_Local
	{
		[Fact]
		public void CalculatorService_PublishLocal()
		{
			try
			{
				var serviceName = nameof(CalculatorService);
				var folderPath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.AppServices.Calculator\bin\Debug\netstandard2.0";

				ServicePublisher.PublishLocal(folderPath, serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
		[Fact]
		public void FileSystemQueryService_PublishLocal()
		{
			try
			{
				var serviceName = nameof(FileSystemQueryService);
				var folderPath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.AppServices.FileSystemQuery\bin\Debug\netstandard2.0";

				ServicePublisher.PublishLocal(folderPath, serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
		[Fact]
		public void ServiceAppConsoleHost_PublishLocal()
		{
			try
			{
				var serviceName = "ServiceAppConsoleHost";
				var folderPath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.ServiceApp.ConsoleHost\bin\Debug\netcoreapp2.1";

				ServicePublisher.PublishLocal(folderPath, serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}

		[Fact]
		public void CalculatorService_RetrieveLocal()
		{
			try
			{
				var serviceName = nameof(CalculatorService);
				var folder = ServiceRetriever.RetrieveLocal(serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
		[Fact]
		public void FileSystemQueryService_RetrieveLocal()
		{
			try
			{
				var serviceName = nameof(FileSystemQueryService);
				var folder = ServiceRetriever.RetrieveLocal(serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
		[Fact]
		public void ServiceAppConsoleHost_RetrieveLocal()
		{
			try
			{
				var serviceName = "ServiceAppConsoleHost";
				var folder = ServiceRetriever.RetrieveLocal(serviceName);
				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
	}
}
