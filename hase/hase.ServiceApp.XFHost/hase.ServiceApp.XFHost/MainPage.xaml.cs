using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace hase.ServiceApp.XFHost
{
    public partial class MainPage : ContentPage
	{
		private IRelayDispatcher<FileSystemQueryService, FileSystemQueryRequest, FileSystemQueryResponse> fsqDispatcher = null;
		private IRelayDispatcher<CalculatorService, CalculatorRequest, CalculatorResponse> calcDispatcher = null;

		public MainPage()
		{
			InitializeComponent();
        }
        private void PressMeButton_Pressed(object sender, EventArgs e)
        {
            (sender as Button).Text = "9";
        }

        private async void PressMeButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;

            button.Text = "Init";
            //Task.Run(() =>
            //{
            HubConnection _hub = null;

            //try
            //{
            //    //button.Text = "Building...";
            //    //Task.Delay(1500).Wait();
            //    _hub = new HubConnectionBuilder()
            //        .WithUrl("http://localhost:5000/route")
            //        .Build();
            //    //button.Text = "Built";
            //    //Task.Delay(1500).Wait();

            //    //button.Text = "Connecting...";
            //    //Task.Delay(1500).Wait();
            //    await _hub.StartAsync();
            //    //button.Text = "Connected";
            //    //Task.Delay(1500).Wait();
            //}
            //catch (Exception ex)
            //{
            //    var txt = ex.Message;
            //    if (ex.InnerException != null)
            //        txt += Environment.NewLine + ex.InnerException.Message;
            //    //button.Text = txt;
            //    //Task.Delay(1500).Wait();
            //}
            //finally
            //{
            //    //button.Text = "Disposing...";
            //    await _hub.DisposeAsync();
            //    //button.Text = "Disposed";
            //}

            try
            {
                var folderPath = @"c:\";
                var fsq = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
                //var fsq = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
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

            //}).Wait();

        }
    }
}
