using hase.DevLib.Framework.Repository.Contract;
using System.Collections.Generic;
using System.IO;

namespace hase.DevLib.Framework.Repository.Service
{
	public static class ServiceRepoJic
	{
		public static string GetFilePath(string fileName, string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ServiceSub(tenantId);
			return Path.Combine(dir.FullName, fileName);
		}
		public static List<string> GetAllFileNames(string tenantId = TenantDir.TenantIdDefault)
		{
			var list = new List<string>();
			var filePaths = GetAllFilePaths(tenantId);
			foreach (var path in filePaths)
			{
				list.Add(Path.GetFileName(path));
			}
			return list;
		}
		private static List<string> GetAllFilePaths(string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ServiceSub(tenantId);
			var list = new List<string>();
			foreach (var file in dir.EnumerateFiles())
			{
				list.Add(file.FullName);
			}
			return list;
		}
		public static List<byte[]> GetAllFiles(string tenantId = TenantDir.TenantIdDefault)
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
		public static byte[] GetFile(string fileName, string tenantId = TenantDir.TenantIdDefault)
		{
			var filePath = GetFilePath(fileName, tenantId);
			var fileBytes = File.ReadAllBytes(filePath);
			return fileBytes;

		}
		public static void SaveFile(string fileName, byte[] fileBytes, string tenantId = TenantDir.TenantIdDefault)
		{
			File.WriteAllBytes(GetFilePath(fileName, tenantId), fileBytes);
		}
		public static void DeleteFile(string fileName, string tenantId = TenantDir.TenantIdDefault)
		{
			File.Delete(GetFilePath(fileName, tenantId));
		}

	}
}
