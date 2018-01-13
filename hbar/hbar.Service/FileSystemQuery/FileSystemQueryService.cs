using System.IO;
using hbar.Contract.FileSystemQuery;

namespace hbar.Service.FileSystemQuery
{
    public class FileSystemQueryService : IFileSystemQueryService
    {
        // todo: consider making this an async method.
        public FileSystemQueryResponse Execute(FileSystemQueryRequest request)
        {
            string responseText = null;
            switch (request.QueryType)
            {
                case FileSystemQueryTypeEnum.DirectoryExists:
                    responseText = Directory.Exists(request.FolderPath).ToString();
                    break;
            }

            var response = new FileSystemQueryResponse(request, responseText);
            return response;
        }
    }
}
