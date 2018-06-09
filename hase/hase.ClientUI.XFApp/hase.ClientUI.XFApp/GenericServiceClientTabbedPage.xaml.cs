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

        void AddClientTabs(IEnumerable<InputFormDef> formDefs)
        {
            foreach (var formDef in formDefs)
            {
                AddGenericClientTab(formDef);
            }
        }
        void AddGenericClientTab(InputFormDef formDef)
        {
            var contentPage = new GenericServiceClientContentPage();
            contentPage.InitializeComponent(formDef);

            this.Children.Add(new NavigationPage(contentPage)
            {
                Title = formDef.NavigationTitle ?? formDef.Name
            });
        }
    }
}