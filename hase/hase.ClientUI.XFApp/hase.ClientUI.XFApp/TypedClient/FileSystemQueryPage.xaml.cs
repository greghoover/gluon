using hase.AppServices.FileSystemQuery.Client;
using hase.AppServices.FileSystemQuery.Contract;
using hase.DevLib.Framework.Relay.Proxy;
using hase.Relays.Signalr.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace hase.ClientUI.XFApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FileSystemQueryPage : ContentPage
	{
		public FileSystemQueryPage ()
		{
			InitializeComponent ();
			this.ResetButton_Clicked(null, null);
		}
		private void ResetButton_Clicked(object sender, EventArgs e)
		{
			this.ServiceLocationPicker.SelectedIndex = 0;
			this.PathEntry.Text = @"c:\";
			this.ResultLabel.Text = string.Empty;
		}
		private void SubmitButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				var client = default(FileSystemQuery);
				switch (this.ServiceLocationPicker.SelectedItem.ToString().ToLower())
				{
					case "local":
						client = new FileSystemQuery();
						break;
					case "remote":
						var hostCfg = new RelayProxyConfig().GetConfigSection();
						var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
						client = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>), proxyCfg);
						break;
					default:
						throw new ApplicationException("Service location must be either Local or Remote.");
				}
				var folderPath = this.PathEntry.Text;
				var result = client.DoesDirectoryExist(folderPath);
				this.ResultLabel.Text = $"Folder path [{folderPath}] {((result ?? false) ? "exists" : "does not exist")}.";
			}
			catch (Exception ex)
			{
				var txt = ex.Message;
				if (ex.InnerException != null)
					txt += Environment.NewLine + ex.InnerException.Message;
				this.ResultLabel.Text = txt;
			}
		}
	}
}