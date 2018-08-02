using hase.DevLib.Framework.Repository.Client;
using hase.DevLib.Framework.Repository.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace hase.ClientUI.XFApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GenericServiceClientTabbedPage : TabbedPage
	{
		public GenericServiceClientTabbedPage()
		{
			InitializeComponent();

			var isOsx = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
			var isIos = RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));
			if (isOsx || isIos)
				AddClientTabs(GetFormDefinitions(@"http://192.168.1.22:5000"));
			else
				AddClientTabs(GetFormDefinitions(@"http://172.27.211.17:5000"));
		}
		public IEnumerable<InputFormDef> GetFormDefinitions(string baseUri)
		{
			if (!baseUri.EndsWith(@"/"))
				baseUri += @"/";
			var requestUri = baseUri + $"api/formdefs";

			HttpClient http = new HttpClient();
			var httpResponse = http.GetAsync(requestUri).Result;
			var responseText = httpResponse.Content.ReadAsStringAsync().Result;

			var jos = JsonConvert.DeserializeObject<IEnumerable<JObject>>(responseText);
			foreach (var jo in jos)
			{
				yield return JsonConvert.DeserializeObject<InputFormDef>(jo.ToString());
			}
		}
		void AddClientTabs(IEnumerable<InputFormDef> formDefs)
		{
			foreach (var formDef in formDefs)
			{
				AddGenericClientTab(formDef);
			}
		}
		void AddGenericClientTab(InputFormDef formDef)
		{
			var contentPage = new GenericServiceClientContentPage();
			contentPage.InitializeComponent(formDef);

			this.Children.Add(new NavigationPage(contentPage)
			{
				Title = formDef.NavigationTitle ?? formDef.Name
			});
		}
	}
}