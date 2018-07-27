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
			var serviceFolders = new List<FolderSpec>();
			var dir = TenantDir.ServiceSub(tenantId);
			foreach (var serviceDir in dir.EnumerateDirectories())
			{
				var serviceFolder = GetFolder(serviceDir.Name, serviceDir);
				serviceFolders.Add(serviceFolder);
			}
			return serviceFolders;
		}
		public static FolderSpec GetFolder(string serviceName, string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ServiceSub(tenantId, serviceName);
			return GetFolder(serviceName, dir);
		}
		public static FolderSpec GetFolder(string serviceName, DirectoryInfo dir)
		{
			var files = new List<FileSpec>();

			foreach (var file in dir.EnumerateFiles())
			{
				var fileBytes = File.ReadAllBytes(file.FullName);
				files.Add(new FileSpec
				{
					FileName = file.Name,
					RelativeSubFolder = new string[] { serviceName },
					Content = fileBytes
				});
			}

			return new FolderSpec
			{
				FolderName = serviceName,
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
