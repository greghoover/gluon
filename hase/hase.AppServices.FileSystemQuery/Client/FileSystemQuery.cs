﻿using hase.AppServices.FileSystemQuery.Contract;
using hase.DevLib.Framework.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace hase.AppServices.FileSystemQuery.Client
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
		public FileSystemQuery(Type proxyType, IConfigurationSection proxyConfig) : base(proxyType, proxyConfig) { }

		public bool? DoesDirectoryExist(string folderPath)
		{
			var request = new FileSystemQueryRequest
			{
				FolderPath = folderPath,
				QueryType = FileSystemQueryTypeEnum.DirectoryExists
			};

			var response = Task.Run<FileSystemQueryResponse>(async () => await this.Service.Execute(request)).Result;
			if (response?.ResponseString == null)
				return null;
			else
			{
				return bool.Parse(response.ResponseString);
			}
		}
	}
}
