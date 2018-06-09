using hase.DevLib.Framework.Contract;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace hase.ClientUI.XFApp
{
    public class GenericServiceClientContentPage : ContentPage
	{
		Button _resetButton;
		Label _descHeader;
		Label _emptyHeader;
		Picker _serviceLocationPicker;
		Button _submitButton;
		Label _resultLabel;
		List<Entry> _entryControls;

		InputFormDef _formDef;

		public GenericServiceClientContentPage() { }

		public void InitializeComponent(InputFormDef definition)
		{
			this._formDef = definition;
			InitKnownControls();
			this.Content = BuildPageContent();
			ResetToInitialValues();
		}

		void InitKnownControls()
		{
			_entryControls = new List<Entry>();

			_resetButton = new Button { Text = "Reset" };
			_resetButton.Clicked += (sender, e) => {
				ResetToInitialValues();
			};

			_descHeader = new Label();
			_emptyHeader = new Label();

			_serviceLocationPicker = new Picker { Title = "Service location:" };
			_serviceLocationPicker.ItemsSource = new List<string> { "Local", "Remote" };

			_submitButton = new Button { Text = "Submit" };
			_submitButton.Clicked += (sender, e) => {
				PerformServiceCall();
			};

			_resultLabel = new Label();
		}
		private void ResetToInitialValues()
		{
			this._serviceLocationPicker.SelectedIndex = 0;

			foreach (var entry in _entryControls)
			{
				entry.Text = entry.Placeholder;
			}
			this._resultLabel.Text = string.Empty;
		}
		private void PerformServiceCall()
		{
			this._resultLabel.Text = $"Service call submitted on {DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")}.";
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
			this.Title = _formDef.ContentTitle ?? _formDef.Name;

			var view = new StackLayout();
			view.Children.Add(_resetButton);
			_descHeader.Text = _formDef.Description;
			view.Children.Add(_descHeader);
			view.Children.Add(_emptyHeader);
			view.Children.Add(_serviceLocationPicker);
			view.Children.Add(_emptyHeader);

			foreach (var field in _formDef.InputFields)
			{
				view.Children.Add(new Label { Text = field.Caption });
				var entry = new Entry { Text = field.DefaultValue, Placeholder = field.DefaultValue };
				_entryControls.Add(entry);
				view.Children.Add(entry);
			}

			view.Children.Add(_submitButton);
			view.Children.Add(_resultLabel);

			return view;
		}
	}
}