using hase.DevLib.Framework.Contract;

namespace hase.AppServices.FileSystemQuery.Contract
{
	public interface IFileSystemQuery : IServiceClient<FileSystemQueryRequest, FileSystemQueryResponse>
    {
        bool? DoesDirectoryExist(string folderPath);
    }
}
