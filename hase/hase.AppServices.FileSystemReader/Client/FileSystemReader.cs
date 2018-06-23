using hase.AppServices.FileSystemReader.Contract;
using hase.DevLib.Framework.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hase.AppServices.FileSystemReader.Client
{
	public class FileSystemReader : ServiceClientBase<FileSystemReaderRequest, FileSystemReaderResponse>, IFileSystemReader
	{
		/// <summary>
		/// Create local service instance.
		/// </summary>
		public FileSystemReader() { }
		/// <summary>
		/// Create proxied service instance.
		/// </summary>
		public FileSystemReader(Type proxyType, IConfigurationSection proxyConfig) : base(proxyType, proxyConfig) { }

		public Dictionary<string, string> GetFolderContents(string folderPath, FileSystemReaderTypeEnum readerType = FileSystemReaderTypeEnum.Shallow)
		{
			var request = new FileSystemReaderRequest
			{
				FolderPath = folderPath,
				ReaderType = readerType,
			};

			var response = Task.Run<FileSystemReaderResponse>(async () => await this.Service.Execute(request)).Result;

			return response?.FolderContents;
		}
	}
}
