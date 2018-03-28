using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;
using Xunit;

namespace hase.DevLib.Tests
{
    public class RelayAndDispatcherFixture : IDisposable
    {
        private NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse> _relay = null;
        private IRelayDispatcherClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse> _dispatcher = null;

        public RelayAndDispatcherFixture()
        {
            Console.WriteLine("Starting Named Pipe Relay.");
            var servicePipeName = typeof(FileSystemQueryService).Name;
            var proxyPipeName = ServiceTypesUtil.GetServiceProxyName(servicePipeName);

            _relay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(servicePipeName, proxyPipeName);
            _relay.StartAsync();
            Console.WriteLine("Named Pipe Relay started.");

            Console.WriteLine("Starting Service Dispatcher");
            _dispatcher = RelayDispatcherClient<NamedPipeRelayDispatcherClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.CreateInstance();
            _dispatcher.StartAsync();
            Console.WriteLine("Service Dispatcher started.");
        }
        public void Dispose()
        {
            Console.WriteLine("Stopping Dispatcher.");
            _dispatcher.StopAsync().Wait();
            Console.WriteLine("Dispatcher stopped.");

            Console.WriteLine("Stopping Relay.");
            _relay.StopAsync().Wait();
            Console.WriteLine("Relay stopped.");
        }
    }

    public class FileSystemQuery_NamedPipeRelayTests : IClassFixture<RelayAndDispatcherFixture>
    {
        [Fact]
        public void VerifyCRootExists_ClientApi_NamedPipeRelay()
        {
            var folderPath = @"c:";
            var fsq = FileSystemQuery.NewWithNamedPipeProxy();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_NamedPipeRelay()
        {
            var folderPath = @"c:";
            var fsqs = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.NewProxied<NamedPipeRelayProxyClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
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
            var fsq = FileSystemQuery.NewWithNamedPipeProxy();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.False(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_NamedPipeRelay()
        {
            var folderPath = @"slkjdfslkj";
            var fsqs = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.NewProxied<NamedPipeRelayProxyClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>>();
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
