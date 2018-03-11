namespace hase.DevLib.Services.FileSystemQuery.Contract
{
    public interface IFileSystemQuery
    {
        string DoesDirectoryExist(string folderPath);
    }
}
