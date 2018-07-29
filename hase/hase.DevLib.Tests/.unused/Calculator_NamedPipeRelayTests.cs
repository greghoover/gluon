//using hase.DevLib.Framework.Relay;
//using hase.DevLib.Framework.Relay.NamedPipe;
//using hase.DevLib.Framework.Service;
//using hase.AppServices.Calculator.Client;
//using hase.AppServices.Calculator.Contract;
//using hase.AppServices.Calculator.Service;
//using System;
//using System.Threading.Tasks;
//using Xunit;

//namespace hase.DevLib.Tests
//{
//    public class CalculatorNamedPipeRelayAndDispatcherFixture : IDisposable
//    {
//        private NamedPipeRelayHub<CalculatorRequest, CalculatorResponse> _calcRelay = null;
//        private IRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse> _calcDispatcher = null;

//        public CalculatorNamedPipeRelayAndDispatcherFixture()
//        {
//            Console.WriteLine("Starting Named Pipe Relay.");
//            var servicePipeName = typeof(CalculatorService).Name;
//            var proxyPipeName = ServiceTypesUtil.GetServiceProxyName(servicePipeName);

//            _calcRelay = new NamedPipeRelayHub<CalculatorRequest, CalculatorResponse>(servicePipeName, proxyPipeName);
//            _calcRelay.StartAsync();
//            Task.Delay(2000).Wait();
//            Console.WriteLine("Named Pipe Relay started.");

//            Console.WriteLine("Starting Service Dispatcher");
//            _calcDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();
//            _calcDispatcher.StartAsync(); // not awaiting on purpose
//            Console.WriteLine("Service Dispatcher started.");
//        }
//        public void Dispose()
//        {
//            Console.WriteLine("Stopping Dispatcher.");
//            _calcDispatcher.StopAsync().Wait(); // use await
//            Console.WriteLine("Dispatcher stopped.");

//            Console.WriteLine("Stopping Relay.");
//            _calcRelay.StopAsync().Wait(); // use await
//            Console.WriteLine("Relay stopped.");
//        }
//    }

//    public class Calculator_NamedPipeRelayTests : IClassFixture<CalculatorNamedPipeRelayAndDispatcherFixture>
//    {
//        [Fact]
//        public void VerifyAddTwoNumbers_ClientApi_NamedPipeRelay()
//        {
//            var n1 = 5;
//            var n2 = 10;
//            var calc = new Calculator(typeof(NamedPipeRelayProxy<CalculatorRequest, CalculatorResponse>));
//            var result = calc.Add(n1, n2);
//            Xunit.Assert.True(result == n1 + n2);
//        }
//        [Fact]
//        public async void VerifyAddTwoNumbers_ServiceApi_NamedPipeRelay()
//        {
//            var n1 = 5;
//            var n2 = 10;
//            var request = new CalculatorRequest(CalculatorOpEnum.Add, n1, n2);
//            var calcService = new Calculator(typeof(NamedPipeRelayProxy<CalculatorRequest, CalculatorResponse>)).Service;
//            var result = await calcService.Execute(request);
//            Xunit.Assert.True(result.Answer == n1 + n2);
//        }
//    }
//}
