using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    public interface IFileSystemQuery //: IService<FileSystemQueryRequest, FileSystemQueryResponse>
    {
        bool DoesDirectoryExist(string folderPath);
    }
}
