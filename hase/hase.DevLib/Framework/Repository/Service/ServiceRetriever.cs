using hase.DevLib.Framework.Repository.Contract;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Repository.Service
{
	public static class ServiceRetriever
	{
		public static FolderSpec RetrieveLocal(string serviceName, string tenantId = TenantDir.TenantIdDefault)
		{
			var folderSpec = ServiceRepo.GetFolder(serviceName, tenantId);
			return folderSpec;
		}
		public static async Task<FolderSpec> RetrieveRemote(string baseUri, string serviceName, string tenantId = TenantDir.TenantIdDefault)
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var requestUri = baseUri + $"api/servicedefs/{serviceName}";

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(requestUri);
				response.EnsureSuccessStatusCode();

				var s2 = await response.Content.ReadAsStringAsync();
				var folder = JsonConvert.DeserializeObject<FolderSpec>(s2);
				return folder;
			}

		}
	}
}
