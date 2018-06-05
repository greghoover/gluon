using hase.DevLib.Framework.Contract._;

namespace hase.DevLib.Services._.FileSystemQuery.Contract
{
    public interface IFileSystemQuery : IServiceClient<FileSystemQueryRequest, FileSystemQueryResponse>
    {
        bool? DoesDirectoryExist(string folderPath);
    }
}
