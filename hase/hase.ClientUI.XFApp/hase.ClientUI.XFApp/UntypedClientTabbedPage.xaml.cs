using hase.DevLib.Framework.Relay.Proxy;
using hase.DevLib.Framework.Repository.Client;
using hase.DevLib.Framework.Repository.Contract;
using hase.Relays.Signalr.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace hase.ClientUI.XFApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UntypedClientTabbedPage : TabbedPage
	{
		public UntypedClientTabbedPage()
		{
			InitializeComponent();

			// todo: 8/25/18 gph. Use separate config for formdef repo.
			// This suffices for now, assuming we are using a signalr relay proxy.
			var hostCfg = new RelayProxyConfig().GetConfigSection();
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
			var signalrCfg = proxyCfg.Get<SignalrRelayProxyConfig>();
			var baseUri = signalrCfg.HubUrl.GetLeftPart(System.UriPartial.Authority);

			AddClientTabs(GetFormDefinitions(baseUri));
		}
		public IEnumerable<InputFormDef> GetFormDefinitions(Uri baseUri)
		{
			return GetFormDefinitions(baseUri.ToString());
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
			var contentPage = new UntypedClientContentPage();
			contentPage.InitializeComponent(formDef);

			this.Children.Add(new NavigationPage(contentPage)
			{
				Title = formDef.NavigationTitle ?? formDef.Name
			});
		}
	}
}