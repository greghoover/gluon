using hase.DevLib.Framework.Client._;
using hase.DevLib.Services._.FileSystemQuery.Contract;
using hase.DevLib.Services._.FileSystemQuery.Service;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Services._.FileSystemQuery.Client
{
    public class FileSystemQuery : ServiceClientBase<FileSystemQueryRequest, FileSystemQueryResponse>, IFileSystemQuery
    {
        /// <summary>
        /// Create local service instance.
        /// </summary>
        public FileSystemQuery() { }
        /// <summary>
        /// Create proxied service instance.
        /// </summary>
        public FileSystemQuery(Type proxyType) : base(proxyType) { }

        public bool? DoesDirectoryExist(string folderPath)
        {
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = Task.Run<FileSystemQueryResponse>(() => Service.Execute(request)).Result;
            if (response?.ResponseString == null)
                return null;
            else
            {
                return bool.Parse(response.ResponseString);
            }
        }
    }
}
