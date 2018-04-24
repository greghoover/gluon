namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    public interface IFileSystemQuery
    {
        bool DoesDirectoryExist(string folderPath);
    }
}
