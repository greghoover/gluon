using hase.DevLib.Framework.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Services.FileSystemQuery.Client
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

            var response = Task.Run<FileSystemQueryResponse>(() => this.Service.Execute(request)).Result;
            if (response?.ResponseString == null)
                return null;
            else
            {
                return bool.Parse(response.ResponseString);
            }
        }
    }
}
