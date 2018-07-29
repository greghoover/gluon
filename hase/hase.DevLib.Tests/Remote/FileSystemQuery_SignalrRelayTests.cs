using hase.AppServices.FileSystemQuery.Client;
using hase.AppServices.FileSystemQuery.Contract;
using hase.DevLib.Framework.Relay.Proxy;
using hase.DevLib.Tests.Fixtures;
using hase.Relays.Signalr.Client;
using hase.Relays.Signalr.Server;
using Xunit;

namespace hase.DevLib.Tests
{
	public class FileSystemQuery_SignalrRelayTests : IClassFixture<SignalrRelayFixture>, IClassFixture<FileSystemQuery_SignalrDispatcherFixture>
	{
		public FileSystemQuery_SignalrRelayTests(SignalrRelayFixture signalrRelayFixture)
		{
			if (signalrRelayFixture.Relay == null)
			{
				var signalrHubCfg = new SignalrRelayHubConfig().GetConfigSection(nameof(FileSystemQuery));
				signalrRelayFixture.StartRelayServer(signalrHubCfg);
			}
		}

		[Fact]
		public void VerifyCRootExists_ClientApi_SignalrRelay()
		{
			var hostCfg = new RelayProxyConfig().GetConfigSection(nameof(FileSystemQuery));
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
			var client = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>), proxyCfg);

			var result = client.DoesDirectoryExist(@"c:");
			Xunit.Assert.True(result);
		}
		[Fact]
		public async void VerifyCRootExists_ServiceApi_SignalrRelay()
		{
			var hostCfg = new RelayProxyConfig().GetConfigSection(nameof(FileSystemQuery));
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
			var service = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>), proxyCfg).Service;

			var request = new FileSystemQueryRequest
			{
				FolderPath = @"c:",
				QueryType = FileSystemQueryTypeEnum.DirectoryExists
			};

			var result = await service.Execute(request);
			var response = result.ResponseString;
			Xunit.Assert.True(bool.Parse(response));
		}
		[Fact]
		public void VerifyBogusPathNotExist_ClientApi_SignalrRelay()
		{
			var hostCfg = new RelayProxyConfig().GetConfigSection(nameof(FileSystemQuery));
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
			var client = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>), proxyCfg);

			var result = client.DoesDirectoryExist("slkdjfslkdflsdfjlsdkjf");
			Xunit.Assert.False(result);
		}
		[Fact]
		public async void VerifyBogusPathNotExist_ServiceApi_SignalrRelay()
		{
			var hostCfg = new RelayProxyConfig().GetConfigSection(nameof(FileSystemQuery));
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
			var service = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>), proxyCfg).Service;

			var request = new FileSystemQueryRequest
			{
				FolderPath = @"slkdjfslkdflsdfjlsdkjf",
				QueryType = FileSystemQueryTypeEnum.DirectoryExists
			};

			var result = await service.Execute(request);
			var response = result.ResponseString;
			Xunit.Assert.False(bool.Parse(response));
		}
	}
}
