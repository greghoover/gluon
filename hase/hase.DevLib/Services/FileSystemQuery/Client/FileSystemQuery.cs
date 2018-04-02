using hase.DevLib.Framework.Client;
using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;

namespace hase.DevLib.Services.FileSystemQuery.Client
{
    public class FileSystemQuery : ServiceClientBase<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, IFileSystemQuery
    {
        public FileSystemQuery() { }

        public FileSystemQuery(Type proxyType) : base(proxyType, ServiceTypesUtil.GetServiceProxyName<FileSystemQueryService>()) { }

        //private FileSystemQuery(IService<FileSystemQueryRequest, FileSystemQueryResponse> service)
        //{
        //    this.Service = service;
        //}

        public static FileSystemQuery NewWithNamedPipeProxy()
        {
            return new FileSystemQuery(typeof(NamedPipeRelayProxyClient<FileSystemQueryRequest, FileSystemQueryResponse>));
        }

        public bool DoesDirectoryExist(string folderPath)
        {
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = Service.Execute(request).ResponseString;
            var result = bool.Parse(response);
            return result;
        }
    }
}
