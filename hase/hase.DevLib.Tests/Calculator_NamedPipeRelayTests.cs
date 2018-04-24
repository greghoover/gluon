using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Service;
using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using System;
using Xunit;

namespace hase.DevLib.Tests
{
    public class CalculatorRelayAndDispatcherFixture : IDisposable
    {
        private NamedPipeRelayHub<CalculatorRequest, CalculatorResponse> _calcRelay = null;
        private IRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse> _calcDispatcher = null;

        public CalculatorRelayAndDispatcherFixture()
        {
            Console.WriteLine("Starting Named Pipe Relay.");
            var servicePipeName = typeof(CalculatorService).Name;
            var proxyPipeName = ServiceTypesUtil.GetServiceProxyName(servicePipeName);

            _calcRelay = new NamedPipeRelayHub<CalculatorRequest, CalculatorResponse>(servicePipeName, proxyPipeName);
            _calcRelay.StartAsync();
            Console.WriteLine("Named Pipe Relay started.");

            Console.WriteLine("Starting Service Dispatcher");
            _calcDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
            _calcDispatcher.StartAsync();
            Console.WriteLine("Service Dispatcher started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping Dispatcher.");
            _calcDispatcher.StopAsync().Wait();
            Console.WriteLine("Dispatcher stopped.");

            Console.WriteLine("Stopping Relay.");
            _calcRelay.StopAsync().Wait();
            Console.WriteLine("Relay stopped.");
        }
    }

    public class Calculator_NamedPipeRelayTests : IClassFixture<CalculatorRelayAndDispatcherFixture>
    {
        [Fact]
        public void VerifyCRootExists_ClientApi_NamedPipeRelay()
        {
            var i1 = 5;
            var i2 = 10;
            var calc = new Calculator(typeof(NamedPipeRelayProxy<CalculatorRequest, CalculatorResponse>));
            var result = calc.Add(i1, i2);
            Xunit.Assert.True(result == i1 + i2);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_NamedPipeRelay()
        {
            var i1 = 5;
            var i2 = 10;
            var request = new CalculatorRequest(CalculatorOpEnum.Add, i1, i2);
            var calcService = new Calculator(typeof(NamedPipeRelayProxy<CalculatorRequest, CalculatorResponse>)).Service;
            var result = calcService.Execute(request).Result;
            Xunit.Assert.True(result == i1 + i2);
        }
    }
}
