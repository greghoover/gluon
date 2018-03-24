using hase.DevLib.Framework.Core;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using Xunit;

namespace hase.DevLib.Tests
{
    public class FileSystemQueryTests
    {
        [Fact]
        public void VerifyCRootExists_ClientApi_LocalInstance()
        {
            var folderPath = @"c:";
            var result = FileSystemQuery.NewLocal().DoesDirectoryExist(folderPath);
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
            var result = FileSystemQuery.NewLocal().DoesDirectoryExist(folderPath);
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

        [Fact]
        public void VerifyCRootExists_ClientApi_NamedPipeRelay()
        {

        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_NamedPipeRelay()
        {

        }

        [Fact]
        public void VerifyBogusPathNotExist_ClientApi_NamedPipeRelay()
        {

        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_NamedPipeRelay()
        {

        }
    }
}
