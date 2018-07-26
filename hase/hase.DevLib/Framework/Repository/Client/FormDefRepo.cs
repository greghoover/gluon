using hase.DevLib.Framework.Repository.Contract;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace hase.DevLib.Framework.Repository.Client
{
	public static class FormDefRepo
	{
		public const string FormDefExtension = "json";

		public static DirectoryInfo GetFolderPath(string tenantId = Repo.TenantIdDflt)
		{
			var dirPath = Path.Combine(Repo.RootDir, tenantId);
			return Directory.CreateDirectory(dirPath);
		}
		public static string GetFilePath(string serviceClientName, string tenantId = Repo.TenantIdDflt)
		{
			var dir = GetFolderPath(tenantId);
			return Path.Combine(dir.FullName, $"{serviceClientName}.{FormDefExtension}");
		}
		public static List<JObject> GetAllFormDefinitions(string tenantId = Repo.TenantIdDflt)
		{
			var dir = GetFolderPath(tenantId);
			var list = new List<JObject>();
			foreach (var file in dir.EnumerateFiles($"*.{FormDefExtension}"))
			{
				var txt = File.ReadAllText(file.FullName);
				var jo = JObject.Parse(txt);
				list.Add(jo);
			}
			return list;
		}
		public static JObject GetFormDefinition(string serviceClientName, string tenantId = Repo.TenantIdDflt)
		{
			var filePath = GetFilePath(serviceClientName, tenantId);
			var txt = File.ReadAllText(filePath);
			return JObject.Parse(txt);

		}
		public static void SaveFormDefinition(string serviceClientName, JObject formDef, string tenantId = Repo.TenantIdDflt)
		{
			File.WriteAllText(GetFilePath(serviceClientName, tenantId), formDef.ToString());
		}
		public static void DeleteFormDefinition(string serviceClientName, string tenantId = Repo.TenantIdDflt)
		{
			File.Delete(GetFilePath(serviceClientName, tenantId));
		}

	}
}
