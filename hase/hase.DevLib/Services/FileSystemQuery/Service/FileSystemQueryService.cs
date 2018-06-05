using hase.DevLib.Framework.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using System.IO;
using System.Threading.Tasks;

namespace hase.DevLib.Services.FileSystemQuery.Service
{
    public class FileSystemQueryService : ServiceBase<FileSystemQueryRequest, FileSystemQueryResponse>
    {
        // todo: consider making this an async method.
        public override Task<FileSystemQueryResponse> Execute(FileSystemQueryRequest request)
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
