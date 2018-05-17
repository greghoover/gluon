using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Tests.Fixtures
{
    public class FileSystemQuery_SignalrDispatcherFixture : IDisposable
    {
        private CancellationToken _ct = new CancellationToken();
        private IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse> _dispatcher = null;

        public FileSystemQuery_SignalrDispatcherFixture()
        {
            Console.WriteLine("Starting Service Dispatcher");
            Task.Run(async () =>
            {
                _dispatcher = RelayDispatcher<SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
                await _dispatcher.StartAsync(_ct);
            });
            Console.WriteLine("Service Dispatcher started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping Dispatcher.");
            Task.Run(async () =>
            {
                if (_dispatcher != null)
                    await _dispatcher.StopAsync(_ct);
            });
            Console.WriteLine("Dispatcher stopped.");
        }
    }
}