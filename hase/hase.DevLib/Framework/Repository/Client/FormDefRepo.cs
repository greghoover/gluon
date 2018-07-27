using hase.DevLib.Framework.Repository.Contract;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace hase.DevLib.Framework.Repository.Client
{
	public static class FormDefRepo
	{
		public const string FormDefExtension = "json";

		public static string GetFilePath(string serviceClientName, string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ClientSub(tenantId);
			return Path.Combine(dir.FullName, $"{serviceClientName}.{FormDefExtension}");
		}
		public static List<JObject> GetAllFormDefinitions(string tenantId = TenantDir.TenantIdDefault)
		{
			var dir = TenantDir.ClientSub(tenantId);
			var list = new List<JObject>();
			foreach (var file in dir.EnumerateFiles($"*.{FormDefExtension}"))
			{
				var txt = File.ReadAllText(file.FullName);
				var jo = JObject.Parse(txt);
				list.Add(jo);
			}
			return list;
		}
		public static JObject GetFormDefinition(string serviceClientName, string tenantId = TenantDir.TenantIdDefault)
		{
			var filePath = GetFilePath(serviceClientName, tenantId);
			var txt = File.ReadAllText(filePath);
			return JObject.Parse(txt);

		}
		public static void SaveFormDefinition(string serviceClientName, JObject formDef, string tenantId = TenantDir.TenantIdDefault)
		{
			File.WriteAllText(GetFilePath(serviceClientName, tenantId), formDef.ToString());
		}
		public static void DeleteFormDefinition(string serviceClientName, string tenantId = TenantDir.TenantIdDefault)
		{
			File.Delete(GetFilePath(serviceClientName, tenantId));
		}

	}
}
