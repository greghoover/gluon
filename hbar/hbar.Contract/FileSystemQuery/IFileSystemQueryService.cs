namespace hbar.Contract.FileSystemQuery
{
    public interface IFileSystemQueryService
    {
        FileSystemQueryResponse Execute(FileSystemQueryRequest request);
    }
}
