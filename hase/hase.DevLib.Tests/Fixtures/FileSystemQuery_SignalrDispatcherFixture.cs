using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
    public class FileSystemQuery_SignalrDispatcherFixture : IDisposable
    {
        private IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse> _dispatcher = null;

        public FileSystemQuery_SignalrDispatcherFixture()
        {
            Console.WriteLine("Starting Service Dispatcher");
            Task.Run(() =>
            {
                _dispatcher = RelayDispatcher<SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
                _dispatcher.StartAsync(); // not awaiting on purpose
            //Task.Delay(2000).Wait();
            });
            Console.WriteLine("Service Dispatcher started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping Dispatcher.");
            Task.Run(async () =>
            {
                if (_dispatcher != null)
                    await _dispatcher.StopAsync();
                //Task.Delay(2000).Wait();
            });
            Console.WriteLine("Dispatcher stopped.");
        }
    }
}