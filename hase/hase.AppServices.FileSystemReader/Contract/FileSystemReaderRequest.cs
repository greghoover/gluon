using hase.DevLib.Framework.Contract;

namespace hase.AppServices.FileSystemReader.Contract
{
	public class FileSystemReaderRequest : AppRequestMessage
	{
		public string FolderPath { get; set; }
		public FileSystemReaderTypeEnum ReaderType { get; set; }

		public FileSystemReaderRequest() { }
		public FileSystemReaderRequest(FileSystemReaderTypeEnum readerType, string folderPath)
		{
			ReaderType = readerType;
			FolderPath = folderPath;
		}

		public override string ToString() =>
			 $"ReaderType[{this.ReaderType}] Path[{this.FolderPath}]";
	}
}
