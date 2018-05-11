using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using Xunit;

namespace hase.DevLib.Tests
{
    public class Calculator_LocalInstanceTests
    {
        [Fact]
        public void VerifyAddTwoNumbers_ClientApi_LocalInstance()
        {
            var client = new Calculator();

            var result = client.Add(5, 10);
            Xunit.Assert.True(result == 15);
        }
        [Fact]
        public async void VerifyAddTwoNumbers_ServiceApi_LocalInstance()
        {
            var service = new Calculator().Service;

            var request = new CalculatorRequest
            {
                I1 = 5,
                I2 = 10,
                Operation = CalculatorOpEnum.Add,
            };

            var result = await service.Execute(request);
            Xunit.Assert.True(result.Answer == 15);
        }
    }
}
