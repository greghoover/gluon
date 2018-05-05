using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using Xunit;

namespace hase.DevLib.Tests
{
    public class Calculator_SignalrRelayTests : IClassFixture<SignalrRelayFixture>, IClassFixture<Calculator_SignalrDispatcherFixture>
    {
        [Fact]
        public void VerifyCRootExists_ClientApi_SignalrRelay()
        {
            var client = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>));

            var result = client.Add(5, 10);
            Xunit.Assert.True(result == 15);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_SignalrRelay()
        {
            var service = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>)).Service;

            var request = new CalculatorRequest
            {
                I1 = 5,
                I2 = 10,
                Operation = CalculatorOpEnum.Add,
            };

            var result = service.Execute(request).Result;
            Xunit.Assert.True(result == 15);
        }
    }
}
