// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Xamarin.Forms.Platform.UWP;

namespace hase.ClientUI.XFApp.UWP2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Xamarin.Forms.Platform.UWP.WindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new hase.ClientUI.XFApp.App());
        }
    }
}
