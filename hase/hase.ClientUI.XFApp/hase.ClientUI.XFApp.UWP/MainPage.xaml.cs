namespace hase.ClientUI.XFApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new hase.ClientUI.XFApp.App());
        }
    }
}
