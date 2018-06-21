using hase.DevLib.Framework.Contract;
using System.Collections.Generic;

namespace hase.AppServices.FileSystemReader.Contract
{
	public class FileSystemReaderResponse : AppResponseMessage
	{
		public Dictionary<string, string> FolderContents { get; set; }

		private FileSystemReaderResponse() { }
		public FileSystemReaderResponse(FileSystemReaderRequest request, Dictionary<string, string> folderContents) : base(request)
		{
			this.FolderContents = folderContents;
		}

		public override string ToString() =>
			$"Request: [{this.AppRequestMessage}] Response: [{(this.FolderContents == null ? "(null)" : "(non-null)")}]";
	}
}
