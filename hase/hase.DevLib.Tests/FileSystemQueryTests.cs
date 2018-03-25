using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using Xunit;

namespace hase.DevLib.Tests
{
    public class FileSystemQueryTests
    {
        #region LocalInstance
        [Fact]
        public void VerifyCRootExists_ClientApi_LocalInstance()
        {
            var folderPath = @"c:";
            var fsq = FileSystemQuery.NewLocal();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_LocalInstance()
        {
            var folderPath = @"c:";
            var fsqs = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.NewLocal();
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
        public void VerifyBogusPathNotExist_ClientApi_LocalInstance()
        {
            var folderPath = @"slkjdfslkj";
            var fsq = FileSystemQuery.NewLocal();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.False(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_LocalInstance()
        {
            var folderPath = @"slkjdfslkj";
            var fsqs = Service<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>.NewLocal();
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = fsqs.Execute(request).ResponseString;
            var result = bool.Parse(response);
            Xunit.Assert.False(result);
        }
        #endregion LocalInstance

        #region NamedPipeRelay
        [Fact]
        public void VerifyCRootExists_ClientApi_NamedPipeRelay()
        {
            // todo: RelayHub and ServiceApp must be running first for this test to pass.
            var folderPath = @"c:";
            var fsq = FileSystemQuery.NewWithNamedPipeProxy();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_NamedPipeRelay()
        {
            // todo: RelayHub and ServiceApp must be running first for this test to pass.
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
            // todo: RelayHub and ServiceApp must be running first for this test to pass.
            var folderPath = @"slkjdfslkj";
            var fsq = FileSystemQuery.NewWithNamedPipeProxy();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.False(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_NamedPipeRelay()
        {
            // todo: RelayHub and ServiceApp must be running first for this test to pass.
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
        #endregion NamedPipeRelay
    }
}
