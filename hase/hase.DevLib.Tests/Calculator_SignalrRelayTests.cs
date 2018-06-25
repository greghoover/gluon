using hase.AppServices.Calculator.Client;
using hase.AppServices.Calculator.Contract;
using hase.DevLib.Framework.Relay.Proxy;
using hase.DevLib.Tests.Fixtures;
using hase.Relays.Signalr.Client;
using Xunit;

namespace hase.DevLib.Tests
{
	// 06/23/18 gph. Pass in real config.
	public class Calculator_SignalrRelayTests : IClassFixture<SignalrRelayFixture>, IClassFixture<Calculator_SignalrDispatcherFixture>
	{
		[Fact]
		public void VerifyAddTwoNumbers_ClientApi_SignalrRelay()
		{
			var hostCfg = new RelayProxyConfig().GetConfigSection(nameof(Calculator));
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
			var client = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>), proxyCfg);

			var result = client.Add(5, 10);
			Xunit.Assert.True(result == 15);
		}
		[Fact]
		public async void VerifyAddTwoNumbers_ServiceApi_SignalrRelay()
		{
			var hostCfg = new RelayProxyConfig().GetConfigSection(nameof(Calculator));
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
			var service = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>), proxyCfg).Service;

			var request = new CalculatorRequest
			{
				Number1 = 5,
				Number2 = 10,
				Operation = CalculatorOpEnum.Add,
			};

			var result = await service.Execute(request);
			Xunit.Assert.True(result.Answer == 15);
		}
	}
}
