using System;
using System.Collections.Generic;
using System.Text;

namespace hase.DevLib.Contract.FileSystemQuery
{
    public interface IFileSystemQuery
    {
        string DoesDirectoryExist(string folderPath);
    }
}
