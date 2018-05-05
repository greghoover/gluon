using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using System.Threading.Tasks;

public class FileSystemQuery_SignalrDispatcherFixture : IDisposable
{
    private IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse> _dispatcher = null;

    public FileSystemQuery_SignalrDispatcherFixture()
    {
        Console.WriteLine("Starting Service Dispatcher");
        Task.Run(() =>
        {
            _dispatcher = RelayDispatcher<SignalrRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
            _dispatcher.StartAsync();
            //Task.Delay(2000).Wait();
        });
        Console.WriteLine("Service Dispatcher started.");
    }
    public void Dispose()
    {
        Console.WriteLine("Stopping Dispatcher.");
        Task.Run(() =>
        {
            if (_dispatcher != null)
                _dispatcher.StopAsync().Wait();
            //Task.Delay(2000).Wait();
        });
        Console.WriteLine("Dispatcher stopped.");
    }
}
