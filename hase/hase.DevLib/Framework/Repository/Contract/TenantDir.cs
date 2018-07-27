using System.Collections.Generic;
using System.IO;

namespace hase.DevLib.Framework.Repository.Contract
{
	public static class TenantDir
	{
		public const string TenantsRootPath = @"C:\ProgramData\hase\tenants";
		public const string TenantIdDefault = "!default";
		private enum TenantSub
		{
			root = 0,
			client = 1,
			service = 2
		}

		/// <summary>
		/// Sub-directory of the tenant tree.
		/// </summary>
		public static DirectoryInfo Sub(string tenantId = TenantDir.TenantIdDefault, params string[] subs)
		{
			return GetTenantDir(tenantId, TenantSub.root, subs);
		}
		/// <summary>
		/// Sub-directory of the tenant client tree.
		/// </summary>
		public static DirectoryInfo ClientSub(string tenantId = TenantDir.TenantIdDefault, params string[] subs)
		{
			return GetTenantDir(tenantId, TenantSub.client, subs);
		}
		/// <summary>
		/// Sub-directory of the tenant service tree.
		/// </summary>
		public static DirectoryInfo ServiceSub(string tenantId = TenantDir.TenantIdDefault, params string[] subs)
		{
			return GetTenantDir(tenantId, TenantSub.service, subs);
		}
		private static DirectoryInfo GetTenantDir(string tenantId = TenantDir.TenantIdDefault, TenantSub sub0 = TenantSub.root, params string[] subs)
		{
			var segs = new List<string>();
			segs.Add(TenantDir.TenantsRootPath); // root
			segs.Add(tenantId); // tenant dir

			if (sub0 != TenantSub.root)
				segs.Add(sub0.ToString()); // tenant subdir

			if (subs != null || subs.Length > 0)
				foreach (var s in subs)
					segs.Add(s); // subtree of tenant subdir

			var dirPath = Path.Combine(segs.ToArray());
			return Directory.CreateDirectory(dirPath);
		}
	}
}
