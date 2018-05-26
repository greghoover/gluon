using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using System;
using Xamarin.Forms;

namespace hase.ClientUI.XFApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
		private void PressMeButton_Pressed(object sender, EventArgs e)
		{
			(sender as Button).Text = "Pressed.";
		}

		private void PressMeButton_Clicked(object sender, EventArgs e)
		{
			var button = (Button)sender;
			button.Text = "Init";

			try
			{
				var folderPath = @"c:\";
				var fsq = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
				var result = fsq.DoesDirectoryExist(folderPath);
				button.Text = $"Does folder [{folderPath}] exist? [{result}].";
			}
			catch (Exception ex)
			{
				var txt = ex.Message;
				if (ex.InnerException != null)
					txt += Environment.NewLine + ex.InnerException.Message;
				button.Text = txt;
			}
			finally
			{
			}

		}
	}
}
