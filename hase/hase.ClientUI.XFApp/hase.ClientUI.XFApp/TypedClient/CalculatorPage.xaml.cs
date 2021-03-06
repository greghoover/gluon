﻿using hase.AppServices.Calculator.Client;
using hase.AppServices.Calculator.Contract;
using hase.DevLib.Framework.Relay.Proxy;
using hase.Relays.Signalr.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace hase.ClientUI.XFApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorPage : ContentPage
	{
		public CalculatorPage ()
		{
			InitializeComponent ();
			this.ResetButton_Clicked(null, null);
		}
		private void ResetButton_Clicked(object sender, EventArgs e)
		{
			this.ServiceLocationPicker.SelectedIndex = 0;
			this.FirstNumber.Text = "0";
			this.SecondNumber.Text = "0";
			this.ResultLabel.Text = string.Empty;
		}
		private void SubmitButton_Clicked(object sender, EventArgs e)
		{
			try
			{
				var client = default(Calculator);
				switch (this.ServiceLocationPicker.SelectedItem.ToString().ToLower())
				{
					case "local":
						client = new Calculator();
						break;
					case "remote":
						var hostCfg = new RelayProxyConfig().GetConfigSection();
						var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);
						client = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>), proxyCfg);
						break;
					default:
						throw new ApplicationException("Service location must be either Local or Remote.");
				}
				var number1 = int.Parse(this.FirstNumber.Text);
				var number2 = int.Parse(this.SecondNumber.Text);
				var result = client.Add(number1, number2);
				this.ResultLabel.Text = $"[{number1}] + [{number2}] = [{result}].";
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