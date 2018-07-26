using hase.DevLib.Framework.Repository.Contract;
using System.Collections.Generic;
using System.IO;

namespace hase.DevLib.Framework.Repository.Service
{
	public static class FileRepo
	{
		public static DirectoryInfo CreateTenantDir(string tenantId = Repo.TenantIdDflt)
		{
			return Directory.CreateDirectory(tenantId);
		}
		public static string GetFilePath(string fileName, string tenantId = Repo.TenantIdDflt, bool createDir = false)
		{
			if (createDir)
				CreateTenantDir(tenantId);

			return Path.Combine(tenantId, fileName);
		}
		public static List<string> GetAllFileNames(string tenantId = Repo.TenantIdDflt)
		{
			var list = new List<string>();
			var filePaths = GetAllFilePaths(tenantId);
			foreach (var path in filePaths)
			{
				list.Add(Path.GetFileName(path));
			}
			return list;
		}
		private static List<string> GetAllFilePaths(string tenantId = Repo.TenantIdDflt)
		{
			var dir = CreateTenantDir(tenantId);
			var list = new List<string>();
			foreach (var file in dir.EnumerateFiles())
			{
				list.Add(file.FullName);
			}
			return list;
		}
		public static List<byte[]> GetAllFiles(string tenantId = Repo.TenantIdDflt)
		{
			var list = new List<byte[]>();
			var filePaths = GetAllFilePaths(tenantId);
			foreach (var path in filePaths)
			{
				var fileBytes = File.ReadAllBytes(path);
				list.Add(fileBytes);
			}
			return list;
		}
		public static byte[] GetFile(string fileName, string tenantId = Repo.TenantIdDflt)
		{
			var filePath = GetFilePath(fileName, tenantId);
			var fileBytes = File.ReadAllBytes(filePath);
			return fileBytes;

		}
		public static void SaveFile(string fileName, byte[] fileBytes, string tenantId = Repo.TenantIdDflt)
		{
			File.WriteAllBytes(GetFilePath(fileName, tenantId, createDir: true), fileBytes);
		}
		public static void DeleteFile(string fileName, string tenantId = Repo.TenantIdDflt)
		{
			File.Delete(GetFilePath(fileName, tenantId));
		}

	}
}
