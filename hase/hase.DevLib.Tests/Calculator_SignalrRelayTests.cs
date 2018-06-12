using System.Threading.Tasks;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Tests.Fixtures;
using Xunit;

namespace hase.DevLib.Tests
{
    public class Calculator_SignalrRelayTests : IClassFixture<SignalrRelayFixture>, IClassFixture<Calculator_SignalrDispatcherFixture>
    {
        [Fact]
        public void VerifyAddTwoNumbers_ClientApi_SignalrRelay()
        {
            var client = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>));

            var result = client.Add(5, 10);
            Xunit.Assert.True(result == 15);
        }
        [Fact]
        public async void VerifyAddTwoNumbers_ServiceApi_SignalrRelay()
        {
            var service = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>)).Service;

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
