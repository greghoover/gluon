using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    public interface IFileSystemQuery : IServiceClient<FileSystemQueryRequest, FileSystemQueryResponse>
    {
        bool? DoesDirectoryExist(string folderPath);
    }
}
