using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace hase.Web.Util
{
	public static class FormDefRepo
	{
		public const string TenantIdDflt = "Tenant0";
		public const string FormDefExtension = "json";

		public static DirectoryInfo CreateTenantDir(string tenantId = TenantIdDflt)
		{
			return Directory.CreateDirectory(tenantId);
		}
		public static string GetFilePath(string serviceClientName, string tenantId = TenantIdDflt, bool createDir = false)
		{
			if (createDir)
				CreateTenantDir(tenantId);

			return Path.Combine(tenantId, $"{serviceClientName}.{FormDefExtension}");
		}
		public static List<JObject> GetAllFormDefinitions(string tenantId = TenantIdDflt)
		{
			var dir = CreateTenantDir(tenantId);
			var jos = new List<JObject>();
			foreach (var file in dir.EnumerateFiles($"*.{FormDefExtension}"))
			{
				var txt = File.ReadAllText(file.FullName);
				var jo = JObject.Parse(txt);
				jos.Add(jo);
			}
			return jos;
		}
		public static JObject GetFormDefinition(string serviceClientName, string tenantId = TenantIdDflt)
		{
			var filePath = GetFilePath(serviceClientName, tenantId);
			var txt = File.ReadAllText(filePath);
			return JObject.Parse(txt);

		}
		public static void SaveFormDefinition(string serviceClientName, JObject formDef, string tenantId = TenantIdDflt)
		{
			File.WriteAllText(GetFilePath(serviceClientName, tenantId, createDir: true), formDef.ToString());
		}
		public static void DeleteFormDefinition(string serviceClientName, string tenantId = TenantIdDflt)
		{
			File.Delete(GetFilePath(serviceClientName, tenantId));
		}

	}
}
