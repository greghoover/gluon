using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using Xunit;

namespace hase.DevLib.Tests
{
    public class Calculator_LocalInstanceTests
    {
        [Fact]
        public void VerifyAddTwoNumbers_ClientApi_LocalInstance()
        {
            var i1 = 5;
            var i2 = 10;
            var calc = new Calculator();
            var result = calc.Add(i1, i2);
            Xunit.Assert.True(result == i1 + i2);
        }
        [Fact]
        public void VerifyAddTwoNumbers_ServiceApi_LocalInstance()
        {
            var i1 = 5;
            var i2 = 10;
            var request = new CalculatorRequest(CalculatorOpEnum.Add, i1, i2);
            var calcService = new CalculatorService();
            var result = calcService.Execute(request).Result;
            Xunit.Assert.True(result == i1 + i2);
        }
    }
}
