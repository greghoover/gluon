using hase.DevLib.Contract.FileSystemQuery;
using System;

namespace hase.DevLib.Service.FileSystemQuery
{
    public class FileSystemQueryFactory
    {
        public static IFileSystemQueryService CreateInstance(bool isRemote = false)
        {
            if (isRemote)
                return new FileSystemQueryProxy();
            else
                return new FileSystemQueryService();
        }
    }
}
