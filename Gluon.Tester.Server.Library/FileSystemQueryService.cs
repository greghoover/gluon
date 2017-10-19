using Gluon.Relay.Contracts;
using Gluon.Tester.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Gluon.Tester.Server.Library
{
    public class FileSystemQueryService : IServiceType, IServiceType<FileSystemQueryRqst, FileSystemQueryRspn>
    {
        // todo: consider making this an async method.
        public FileSystemQueryRspn Execute(FileSystemQueryRqst request)
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

        // todo: Eliminate the untyped Execute method, or at least move it to
        // a more centralized area. Don't want this noise in every service class.
        public void Execute(ICommunicationClient hub, object request)
        {
            var json = request as JObject;
            var rqst = json.ToObject<FileSystemQueryRqst>();
            //return Execute(hub, rqst);
            var response = Execute(rqst);
            Console.WriteLine(response);
            hub.InvokeAsync(CX.RelayResponseMethodName, response.CorrelationId, response).Wait();
        }
    }
}
