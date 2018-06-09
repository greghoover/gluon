using hase.DevLib.Framework.Contract;
using System.Collections.Generic;
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

            AddClientTabs(ServiceDefinitions.GetAll());
        }

        void AddClientTabs(IEnumerable<InputFormDef> list)
        {
            foreach (var definition in list)
            {
                AddGenericClientTab(definition);
            }
        }
        void AddGenericClientTab(InputFormDef definition)
        {
            var contentPage = new GenericServiceClientContentPage();
            contentPage.InitializeComponent(definition);

            this.Children.Add(new NavigationPage(contentPage)
            {
                Title = definition.NavigationTitle
            });
        }
    }
}