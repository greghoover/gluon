using hase.DevLib.Framework.Client;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;

namespace hase.DevLib.Services.FileSystemQuery.Client
{
    public class FileSystemQuery : ServiceClientBase<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse>, IFileSystemQuery
    {
        /// <summary>
        /// Create local service instance.
        /// </summary>
        public FileSystemQuery() { }
        /// <summary>
        /// Create proxied service instance.
        /// </summary>
        public FileSystemQuery(Type proxyType) : base(proxyType, ServiceTypesUtil.GetServiceProxyName<FileSystemQueryService>()) { }
        /// <summary>
        /// Use provided service instance.
        /// </summary>
        public FileSystemQuery(IService<FileSystemQueryRequest, FileSystemQueryResponse> service) : base(service) { }

        public bool DoesDirectoryExist(string folderPath)
        {
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = Service.Execute(request);
            if (response == null)
                return false;
            else
            {
                var responseString = response.ResponseString;
                var result = bool.Parse(responseString);
                return result;
            }
        }
    }
}
