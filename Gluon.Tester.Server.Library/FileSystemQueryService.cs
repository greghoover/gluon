using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Contracts;
using System.IO;

namespace Gluon.Tester.Server.Library
{
    public class FileSystemQueryService : RequestResponseServiceBase<FileSystemQueryRqst, FileSystemQueryRspn>
    {
        // todo: consider making this an async method.
        public override FileSystemQueryRspn Execute(FileSystemQueryRqst request)
        {
            string responseText = null;
            switch (request.QueryType)
            {
                case FileSystemQueryTypeEnum.DirectoryExists:
                    responseText = Directory.Exists(request.FolderPath).ToString();
                    break;
            }

            var response = new FileSystemQueryRspn(request, responseText);
            return response;
        }
    }
}
