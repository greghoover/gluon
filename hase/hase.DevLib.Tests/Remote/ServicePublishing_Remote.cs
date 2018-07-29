using hase.AppServices.Calculator.Service;
using hase.AppServices.FileSystemQuery.Service;
using hase.DevLib.Framework.Repository.Service;
using hase.DevLib.Tests.Fixtures;
using hase.Relays.Signalr.Server;
using System;
using Xunit;

namespace hase.DevLib.Tests
{
	public class ServicePublishing_Remote : IClassFixture<SignalrRelayFixture>
	{
		//string baseUri = null;
		string baseUri = @"http://172.27.211.17:5000";
		public ServicePublishing_Remote(SignalrRelayFixture signalrRelayFixture)
		{
			if (signalrRelayFixture.Relay == null)
			{
				var signalrHubCfg = new SignalrRelayHubConfig().GetConfigSection(nameof(ServicePublishing_Remote));
				signalrRelayFixture.StartRelayServer(signalrHubCfg);

				//baseUri = signalrRelayFixture?.GetBaseUri(signalrHubCfg?.HubUrl[0])?.ToString();
			}
		}

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
