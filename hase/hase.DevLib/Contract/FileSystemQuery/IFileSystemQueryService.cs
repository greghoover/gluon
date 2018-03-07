namespace hase.DevLib.Contract.FileSystemQuery
{
    public interface IFileSystemQueryService
    {
        FileSystemQueryResponse Execute(FileSystemQueryRequest request);
    }
}
