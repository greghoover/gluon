using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using Xunit;

namespace hase.DevLib.Tests
{
    public class FileSystemQuery_SignalrRelayTests : IClassFixture<SignalrRelayFixture>, IClassFixture<FileSystemQuery_SignalrDispatcherFixture>
    {
        [Fact]
        public void VerifyCRootExists_ClientApi_SignalrRelay()
        {
            var client = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));

            var result = client.DoesDirectoryExist(@"c:");
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_SignalrRelay()
        {
            var service = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>)).Service;

            var request = new FileSystemQueryRequest
            {
                FolderPath = @"c:",
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = service.Execute(request).ResponseString;
            var result = bool.Parse(response);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ClientApi_SignalrRelay()
        {
            var client = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));

            var result = client.DoesDirectoryExist("slkdjfslkdflsdfjlsdkjf");
            Xunit.Assert.False(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_SignalrRelay()
        {
            var service = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>)).Service;

            var request = new FileSystemQueryRequest
            {
                FolderPath = @"slkdjfslkdflsdfjlsdkjf",
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = service.Execute(request).ResponseString;
            var result = bool.Parse(response);
            Xunit.Assert.False(result);
        }
    }
}
