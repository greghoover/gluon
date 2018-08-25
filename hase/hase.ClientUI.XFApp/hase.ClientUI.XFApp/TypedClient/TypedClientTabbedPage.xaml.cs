using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace hase.ClientUI.XFApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TypedClientTabbedPage : TabbedPage
    {
        public TypedClientTabbedPage ()
        {
            InitializeComponent();
        }
    }
}