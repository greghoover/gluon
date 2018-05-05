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
            var i1 = 5;
            var i2 = 10;
            var calc = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>));
            var result = calc.Add(i1, i2);
            Xunit.Assert.True(result == i1 + i2);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_SignalrRelay()
        {
            var i1 = 5;
            var i2 = 10;
            var request = new CalculatorRequest(CalculatorOpEnum.Add, i1, i2);
            var calcService = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>)).Service;
            var result = calcService.Execute(request).Result;
            Xunit.Assert.True(result == i1 + i2);
        }
    }
}
