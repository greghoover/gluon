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
            var folderPath = @"c:";
            var fsq = new FileSystemQuery();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.True(result);
        }
        [Fact]
        public void VerifyCRootExists_ServiceApi_LocalInstance()
        {
            var folderPath = @"c:";
            var fsqs = new FileSystemQuery().Service;
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
            var fsq = new FileSystemQuery();
            var result = fsq.DoesDirectoryExist(folderPath);
            Xunit.Assert.False(result);
        }
        [Fact]
        public void VerifyBogusPathNotExist_ServiceApi_LocalInstance()
        {
            var folderPath = @"slkjdfslkj";
            var fsqs = new FileSystemQuery().Service;
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
