using hase.AppServices.FileSystemReader.Contract;
using hase.DevLib.Framework.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace hase.AppServices.FileSystemReader.Service
{
	public class FileSystemReaderService : ServiceBase<FileSystemReaderRequest, FileSystemReaderResponse>
	{
		// todo: consider making this an async method.
		public override Task<FileSystemReaderResponse> Execute(FileSystemReaderRequest request)
		{
			Dictionary<string, string> folderContents = null;
			switch (request.ReaderType)
			{
				//case FileSystemReaderTypeEnum.Deep:
				//	throw new NotSupportedException(nameof(FileSystemReaderTypeEnum.Deep));
				case FileSystemReaderTypeEnum.Shallow:
					folderContents = GetFilesInFolder(request.FolderPath);
					break;
			}

			var response = new FileSystemReaderResponse(request, folderContents);
			return Task.FromResult(response);
		}

		private Dictionary<string, string> GetFilesInFolder(string folderPath)
		{
			if (!Directory.Exists(folderPath))
				return null;

			var files = new Dictionary<string, string>();
			foreach (var filePath in Directory.GetFiles(folderPath))
			{
				files.Add(filePath, "fake content text"); // File.ReadAllText(filePath));
			}
			return files;
		}
	}
}
