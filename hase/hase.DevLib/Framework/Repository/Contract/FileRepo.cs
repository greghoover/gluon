using System.IO;

namespace hase.DevLib.Framework.Repository.Contract
{
	public static class FileRepo
	{
		public static FileSpec GetFile(string fileName, string tenantId = TenantDir.TenantIdDefault, params string[] subs)
		{
			var filePath = TenantDir.ServiceSub(tenantId, subs).FullName;
			var fileBytes = File.ReadAllBytes(filePath);
			return new FileSpec
			{
				FileName = fileName,
				RelativeSubFolder = subs,
				Content = fileBytes
			};
		}
		public static void SaveFile(FileSpec file, string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ServiceSub(tenantId, file.RelativeSubFolder);
			var filePath = Path.Combine(dir.FullName, file.FileName);
			File.WriteAllBytes(filePath, file.Content);
		}
		public static void DeleteFile(FileSpec file, string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ServiceSub(tenantId, file.RelativeSubFolder);
			var filePath = Path.Combine(dir.FullName, file.FileName);
			File.Delete(filePath);
		}
	}
}
