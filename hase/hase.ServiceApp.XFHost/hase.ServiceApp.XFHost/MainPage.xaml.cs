using System;
using Xamarin.Forms;

namespace hase.ServiceApp.XFHost
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
            (sender as Button).Text = "Clicked.";
        }
    }
}
