using hase.DevLib.Framework.Repository.Contract;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Repository.Service
{
	public static class ServicePublisher
	{
		/// <summary>
		/// Publish to local file system.
		/// </summary>
		/// <param name="folderPath">The root folder path to publish.</param>
		/// <param name="altFolderName">Specify to make different from actual folder name.</param>
		public static FolderSpec PublishLocal(string folderPath, string altFolderName = null)
		{
			var folderSpec = ServiceRepo.GetFolder(new DirectoryInfo(folderPath), altFolderName);
			ServiceRepo.SaveFolder(folderSpec);
			return folderSpec;
		}
		/// <summary>
		/// Publish to via a web api call.
		/// </summary>
		/// <param name="folderPath">The root folder path to publish.</param>
		/// <param name="altFolderName">Specify to make different from actual folder name.</param>
		public static async Task<FolderSpec> PublishRemote(string baseUri, string folderPath, string altFolderName = null)
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var requestUri = baseUri + $"api/servicedefs";

			var folderSpec = ServiceRepo.GetFolder(new DirectoryInfo(folderPath), altFolderName);

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var s1 = JsonConvert.SerializeObject(folderSpec);
				var content = new StringContent(s1);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var response = await client.PostAsync(requestUri, content);

				//response.EnsureSuccessStatusCode();
				var s2 = await response.Content.ReadAsStringAsync();
				var posted = JsonConvert.DeserializeObject<FolderSpec>(s2);
				return posted;
			}

		}
	}
}
