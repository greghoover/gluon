using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace hase.ClientUI.XFApp
{
	public class GenericServiceClientContentPage : ContentPage
	{
		Button ResetButton;
		Label InfoHeader;
		Label EmptyHeader;
		Picker ServiceLocationPicker;
		Button SubmitButton;
		Label ResultLabel;
		List<Entry> EntryViews;

		ServiceDefinition Definition;

		public GenericServiceClientContentPage() { }

		public void InitializeComponent(ServiceDefinition definition)
		{
			this.Definition = definition;
			InitKnownControls();
			this.Content = BuildPageContent();
			ResetToInitialValues();
		}

		void InitKnownControls()
		{
			EntryViews = new List<Entry>();

			ResetButton = new Button { Text = "Reset" };
			ResetButton.Clicked += (sender, e) => {
				ResetToInitialValues();
			};

			InfoHeader = new Label();
			EmptyHeader = new Label();

			ServiceLocationPicker = new Picker { Title = "Service location:" };
			ServiceLocationPicker.ItemsSource = new List<string> { "Local", "Remote" };

			SubmitButton = new Button { Text = "Submit" };
			SubmitButton.Clicked += (sender, e) => {
				PerformServiceCall();
			};

			ResultLabel = new Label();
		}
		private void ResetToInitialValues()
		{
			this.ServiceLocationPicker.SelectedIndex = 0;

			foreach (var entry in EntryViews)
			{
				entry.Text = entry.Placeholder;
			}
			this.ResultLabel.Text = string.Empty;
		}
		private void PerformServiceCall()
		{
			this.ResultLabel.Text = $"Service call submitted on {DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")}.";
			//try
			//{
			//    var calc = default(Calculator);
			//    switch (this.ServiceLocationPicker.SelectedItem.ToString().ToLower())
			//    {
			//        case "local":
			//            calc = new Calculator();
			//            break;
			//        case "remote":
			//            calc = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>));
			//            break;
			//        default:
			//            throw new ApplicationException("Service location must be either Local or Remote.");
			//    }
			//    var number1 = int.Parse(this.FirstNumber.Text);
			//    var number2 = int.Parse(this.SecondNumber.Text);
			//    var result = calc.Add(number1, number2);
			//    this.ResultLabel.Text = $"[{number1}] + [{number2}] = [{result}].";
			//}
			//catch (Exception ex)
			//{
			//    var txt = ex.Message;
			//    if (ex.InnerException != null)
			//        txt += Environment.NewLine + ex.InnerException.Message;
			//    this.ResultLabel.Text = txt;
			//}
			//finally
			//{
			//}
		}

		public View BuildPageContent()
		{
			this.Title = Definition.ContentTitle;

			var view = new StackLayout();
			view.Children.Add(ResetButton);
			InfoHeader.Text = Definition.InfoHeader;
			view.Children.Add(InfoHeader);
			view.Children.Add(EmptyHeader);
			view.Children.Add(ServiceLocationPicker);
			view.Children.Add(EmptyHeader);

			foreach (var field in Definition.Fields)
			{
				view.Children.Add(new Label { Text = field.Caption });
				var entry = new Entry { Text = field.DefaultValue, Placeholder = field.DefaultValue };
				EntryViews.Add(entry);
				view.Children.Add(entry);
			}

			view.Children.Add(SubmitButton);
			view.Children.Add(ResultLabel);

			return view;
		}
	}
}