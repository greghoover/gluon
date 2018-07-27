using hase.DevLib.Framework.Repository.Service;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Utility
{
	public static class Util
	{
		public static async Task<TBase> GeneralizeTask<TBase, TDerived>(Task<TDerived> task)
			where TDerived : TBase
		{
			return (TBase)await task;
		}
		public static async Task<HttpResponseMessage> PostDoc(string baseUri, CreateDocumentModel model)
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var requestUri = baseUri + $"api/doc";

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var s1 = JsonConvert.SerializeObject(model);
				var content = new StringContent(s1);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var response = await client.PostAsync(requestUri, content);
				//response.EnsureSuccessStatusCode();
				var s2 = await response.Content.ReadAsStringAsync();
				//return JsonConvert.DeserializeObject<CreateDocumentModel>(s2);
				return response;
			}
		}
		public static async Task<CreateDocumentModel> GetDoc(string baseUri, string fileName)
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var requestUri = baseUri + $"api/doc/{fileName}";

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(requestUri);
				//response.EnsureSuccessStatusCode();
				var s = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<CreateDocumentModel>(s);
			}
		}
		public static async Task<byte[]> GetFileBytes(string baseUri, string fileName)
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var requestUri = baseUri + $"api/files/{fileName}";

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

				var response = await client.GetAsync(requestUri);

				response.EnsureSuccessStatusCode();

				return await response.Content.ReadAsByteArrayAsync();
			}
		}
		public static async Task<HttpResponseMessage> PutFileBytesAsync(string baseUri, string filePath)
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var fileName = Path.GetFileName(filePath);
			var requestUri = baseUri + $"api/files/{fileName}";

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

				var fileBytes = File.ReadAllBytes(filePath);
				var byteArrayContent = new ByteArrayContent(fileBytes);
				byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

				var response = await client.PutAsync(requestUri, byteArrayContent);

				response.EnsureSuccessStatusCode();

				return response;
			}
		}
		//public static byte[] SerializeObjectToBson<T>(T obj)
		//{
		//	using (MemoryStream ms = new MemoryStream())
		//	{
		//		using (BsonDataWriter writer = new BsonDataWriter(ms))
		//		{
		//			JsonSerializer serializer = new JsonSerializer();
		//			serializer.Serialize(writer, obj);
		//		}

		//		return ms.ToArray();
		//	}
		//}
		//public static async Task<HttpResponseMessage> PostObjectAsBsonAsync<T>(string url, T data)
		//{
		//	using (var client = new HttpClient())
		//	{
		//		//Specifiy 'Accept' header As BSON: to ask server to return data as BSON format
		//		client.DefaultRequestHeaders.Accept.Clear();
		//		client.DefaultRequestHeaders.Accept.Add(
		//				new MediaTypeWithQualityHeaderValue("application/bson"));

		//		//Specify 'Content-Type' header: to tell server which format of the data will be posted
		//		//Post data will be as Bson format                
		//		var bSonData = Util.SerializeObjectToBson<T>(data);
		//		var byteArrayContent = new ByteArrayContent(bSonData);
		//		byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/bson");

		//		var response = await client.PostAsync(url, byteArrayContent);

		//		response.EnsureSuccessStatusCode();

		//		return response;
		//	}
		//}
	}
}
