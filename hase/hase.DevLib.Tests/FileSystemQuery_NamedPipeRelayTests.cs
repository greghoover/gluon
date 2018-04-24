using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Service;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using Xunit;

namespace hase.DevLib.Tests
{
    public class FileSystemQueryRelayAndDispatcherFixture : IDisposable
    {
        private NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse> _fsqRelay = null;
        private IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse> _fsqDispatcher = null;

        public FileSystemQueryRelayAndDispatcherFixture()
        {
            Console.WriteLine("Starting Named Pipe Relay.");
            var servicePipeName = typeof(FileSystemQueryService).Name;
            var proxyPipeName = ServiceTypesUtil.GetServiceProxyName(servicePipeName);

            _fsqRelay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(servicePipeName, proxyPipeName);
            _fsqRelay.StartAsync();
            Console.WriteLine("Named Pipe Relay started.");

            Console.WriteLine("Starting Service Dispatcher");
            _fsqDispatcher = RelayDispatcher<NamedPipeRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
            _fsqDispatcher.StartAsync();
            Console.WriteLine("Service Dispatcher started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping Dispatcher.");
            _fsqDispatcher.StopAsync().Wait();
            Console.WriteLine("Dispatcher stopped.");

            Console.WriteLine("Stopping Relay.");
            _fsqRelay.StopAsync().Wait();
            Console.WriteLine("Relay stopped.");
        }
    }

    public class FileSystemQuery_NamedPipeRelayTests : IClassFixture<FileSystemQueryRelayAndDispatcherFixture>
    {
        [Fact]
        public void VerifyCRootExists_ClientApi_NamedPipeRelay()
        {
            var folderPath = @"c:";
            var fsq = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_NamedPipeRelay()
        {
            var folderPath = @"c:";
            var fsqs = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>)).Service;
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = fsqs.Execute(request).ResponseString;
            var result = bool.Parse(response);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ClientApi_NamedPipeRelay()
        {
            var folderPath = @"slkjdfslkj";
            var fsq = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.False(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_NamedPipeRelay()
        {
            var folderPath = @"slkjdfslkj";
            var fsqs = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>)).Service;
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = fsqs.Execute(request).ResponseString;
            var result = bool.Parse(response);
            Xunit.Assert.False(result);
        }
    }
}
