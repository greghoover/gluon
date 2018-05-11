using System.IO;
using System.Threading.Tasks;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Services.FileSystemQuery.Contract;

namespace hase.DevLib.Services.FileSystemQuery.Service
{
    public class FileSystemQueryService : IService<FileSystemQueryRequest, FileSystemQueryResponse>
    {
        // todo: consider making this an async method.
        public Task<FileSystemQueryResponse> Execute(FileSystemQueryRequest request)
        {
            string responseText = null;
            switch (request.QueryType)
            {
                case FileSystemQueryTypeEnum.DirectoryExists:
                    responseText = Directory.Exists(request.FolderPath).ToString();
                    break;
            }

            var response = new FileSystemQueryResponse(request, responseText);
            return Task.FromResult(response);
        }
    }
}
