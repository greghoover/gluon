﻿using hase.DevLib.Framework.Client;
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
        public FileSystemQuery(Type proxyType) : base(proxyType) { }

        public bool? DoesDirectoryExist(string folderPath)
        {
            var request = new FileSystemQueryRequest
            {
                FolderPath = folderPath,
                QueryType = FileSystemQueryTypeEnum.DirectoryExists
            };

            var response = Service.Execute(request);

            if (response?.ResponseString == null)
                return null;
            else
            {
                return bool.Parse(response.ResponseString);
            }
        }
    }
}
