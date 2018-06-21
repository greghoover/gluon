using hase.DevLib.Framework.Contract;
using System.Collections.Generic;

namespace hase.AppServices.FileSystemReader.Contract
{
	public interface IFileSystemReader : IServiceClient<FileSystemReaderRequest, FileSystemReaderResponse>
	{
		Dictionary<string, string> GetFolderContents(string folderPath, FileSystemReaderTypeEnum readerType = FileSystemReaderTypeEnum.Shallow);
	}
}
