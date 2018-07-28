using hase.DevLib.Framework.Repository.Contract;
using System;
using System.Collections.Generic;
using System.IO;

namespace hase.DevLib.Framework.Repository.Service
{
	public static class ServiceRepo
	{
		public static IEnumerable<FolderSpec> GetAllFolders(string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ServiceSub(tenantId);
			return GetAllFolders(dir);
		}
		public static IEnumerable<FolderSpec> GetAllFolders(DirectoryInfo baseDir)
		{
			var folders = new List<FolderSpec>();
			foreach (var dir in baseDir.EnumerateDirectories())
			{
				var serviceFolder = GetFolder(dir);
				folders.Add(serviceFolder);
			}
			return folders;
		}
		/// /// <summary>
		/// Use to retrieve a service folder from a repository.
		/// </summary>
		/// <param name="folderName">The name used when it was published.</param>
		/// <param name="tenantId"></param>
		/// <returns></returns>
		public static FolderSpec GetFolder(string folderName, string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ServiceSub(tenantId, folderName);
			return GetFolder(dir); //, folderName);
		}
		/// <summary>
		/// Use to publish a service folder to a repository, or when the full folder path is known.
		/// </summary>
		/// <param name="dir">The root folder to publish.</param>
		/// <param name="altFolderName">Specify to make different from actual folder name.</param>
		/// <returns></returns>
		public static FolderSpec GetFolder(DirectoryInfo dir, string altFolderName = null)
		{
			var folderName = altFolderName;
			if (string.IsNullOrWhiteSpace(folderName))
				folderName = dir.Name;

			var files = new List<FileSpec>();

			foreach (var file in dir.EnumerateFiles())
			{
				var fileBytes = File.ReadAllBytes(file.FullName);
				files.Add(new FileSpec
				{
					FileName = file.Name,
					RelativeSubFolder = new string[] { folderName }, // Currently only supports 1 level deep.
					Content = fileBytes
				});
			}

			return new FolderSpec
			{
				FolderName = folderName,
				Files = files
			};
		}
		public static void SaveFolder(FolderSpec folder, string tenantId = TenantDir.TenantIdDefault)
		{
			foreach (var file in folder.Files)
			{
				if (file.RelativeSubFolder == null || file.RelativeSubFolder.Length < 1)
				{
					file.RelativeSubFolder = new string[] { folder.FolderName };
				}
				FileRepo.SaveFile(file, tenantId);
			}
		}
		public static void DeleteFolder(string serviceName, string tenantId = TenantDir.TenantIdDefault)
		{
			if (string.IsNullOrWhiteSpace(serviceName))
				throw new ArgumentNullException("serviceName");

			var dir = TenantDir.ServiceSub(tenantId, serviceName);
			Directory.Delete(dir.FullName);
		}
	}
}
