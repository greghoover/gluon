using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using Xunit;

namespace hase.DevLib.Tests
{
    public class FileSystemQuery_LocalInstanceTests
    {
        [Fact]
        public void VerifyCRootExists_ClientApi_LocalInstance()
        {
            var client = new FileSystemQuery();

            var result = client.DoesDirectoryExist(@"c:");
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_LocalInstance()
        {
            var service = new FileSystemQuery().Service;

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
        public void VerifyBogusPathNotExist_ClientApi_LocalInstance()
        {
            var client = new FileSystemQuery();

            var result = client.DoesDirectoryExist("slkdjfslkdflsdfjlsdkjf");
            Xunit.Assert.False(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_LocalInstance()
        {
            var service = new FileSystemQuery().Service;

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
