using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gluon.Relay.Contracts;
using Gluon.Relay.Signalr.Client;

namespace hase.ServiceApp.XFHost
{
	public partial class MainPage : ContentPage
	{
		private IRelayDispatcherClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse> fsqDispatcher = null;
		private IRelayDispatcherClient<CalculatorService, CalculatorRequest, CalculatorResponse> calcDispatcher = null;

		public MainPage()
		{
			InitializeComponent();

			var instanceId = "FileSystemQueryServiceHost";
			var qs = $"?{ClientIdTypeEnum.ClientId}={instanceId}";
			var subscriptionChannel = (@"http://localhost:5000/messagehub") + qs;
			var proxy = new ServiceHostRelayProxy(instanceId, subscriptionChannel);


			//Task.Run(() => {
			//	fsqDispatcher = RelayDispatcherClient<NamedPipeRelayDispatcherClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
			//	//calcDispatcher = RelayDispatcherClient<NamedPipeRelayDispatcherClient<CalculatorService, CalculatorRequest, CalculatorResponse>, CalculatorService, CalculatorRequest, CalculatorResponse>.CreateInstance();

			//	Console.WriteLine("Starting Service Dispatcher");
			//	fsqDispatcher.StartAsync();
			//	//calcDispatcher.StartAsync();
			//	Console.WriteLine("Service Dispatcher started.");

			//	//Console.WriteLine("Press <Enter> to stop dispatcher.");
			//	//Console.ReadLine();
			//	//fsqDispatcher.StopAsync().Wait();
			//	//calcDispatcher.StopAsync().Wait();
			//	//Console.WriteLine("Dispatcher stopped.");

			//	//Console.Write("Press <Enter> to close window.");
			//	//Console.ReadLine();
			//});
		}
	}
}
