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
	public class ServicePublishing_Local
	{
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
	}
}
