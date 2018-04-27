using hase.DevLib.Framework.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;

namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    public interface IFileSystemQuery : IServiceClient<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>
    {
        bool DoesDirectoryExist(string folderPath);
    }
}
