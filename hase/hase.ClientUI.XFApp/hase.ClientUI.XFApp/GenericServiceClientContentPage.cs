using hase.DevLib.Framework.Client;
using hase.DevLib.Framework.Contract;
using hase.DevLib.Framework.Relay.Signalr;
using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.FileSystemQuery.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using static hase.DevLib.Framework.Client.ClientUtil;

namespace hase.ClientUI.XFApp
{
	public class GenericServiceClientContentPage : ContentPage
	{
		Button resetButton;
		Label descHeader;
		Label emptyHeader;
		Picker serviceLocationPicker;
		Button submitButton;
		Label resultLabel;
		List<Entry> entryControls;
		List<Picker> pickerControls;

		InputFormDef formDef;

		public GenericServiceClientContentPage() { }

		public void InitializeComponent(InputFormDef formDef)
		{
			this.formDef = formDef;
			InitKnownControls();
			this.Content = BuildPageContent();
			ResetToInitialValues();
		}

		void InitKnownControls()
		{
			this.entryControls = new List<Entry>();
			this.pickerControls = new List<Picker>();

			this.resetButton = new Button { Text = "Reset" };
			this.resetButton.Clicked += (sender, e) => {
				ResetToInitialValues();
			};

			this.descHeader = new Label();
			this.emptyHeader = new Label();

			this.serviceLocationPicker = new Picker { Title = "Service location:" };
			//this.serviceLocationPicker.ItemsSource = new List<string> { "Local", "Remote" };
			//this.serviceLocationPicker.ItemsSource = (IList)ClientUtil.GetReadableEnumNames<ServiceLocation>();
			this.serviceLocationPicker.ItemsSource = Enum.GetNames(typeof(ServiceLocation));

			this.submitButton = new Button { Text = "Submit" };
			this.submitButton.Clicked += (sender, e) => {
				PerformServiceCall();
			};

			this.resultLabel = new Label();
		}
		private void ResetToInitialValues()
		{
			//this.serviceLocationPicker.SelectedIndex = 0;
			SelectPickerItem(this.serviceLocationPicker, "Remote");

			foreach (var entry in this.entryControls)
			{
				entry.Text = entry.Placeholder;
			}

			foreach (var picker in this.pickerControls)
			{
				SelectPickerItem(picker, picker.ClassId);
			}

			this.resultLabel.Text = string.Empty;
		}
		IDictionary<string, string> ExtractInputValues()
		{
			var vals = new Dictionary<string, string>();

			foreach (var entry in this.entryControls)
			{
				vals.Add(entry.StyleId, entry.Text);
			}
			foreach (var picker in this.pickerControls)
			{
				vals.Add(picker.StyleId, picker.SelectedItem.ToString());
			}
			return vals;
		}
		private void PerformServiceCall()
		{
			try
			{
				var serviceName = ContractUtil.EnsureServiceSuffix(this.formDef.Name);
				var proxyName = ContractUtil.EnsureProxySuffix(this.formDef.Name);
				var client = default(IServiceClient<AppRequestMessage, AppResponseMessage>);

				var selectedItem = (ServiceLocation) Enum.Parse(typeof(ServiceLocation), this.serviceLocationPicker.SelectedItem.ToString());
				switch (selectedItem)
				{
					case ServiceLocation.Local:
						//switch (this.formDef.Name)
						//{
						//	case "Calculator":
						//		var calc = new Calculator();
						//		break;
						//	case "FileSystemQuery":
						//		var fsq = new FileSystemQuery();
						//		break;
						//}
						throw new ApplicationException("Untyped service client is not compatible with local service instances. Use remote service instance instead.");
						break;
					case ServiceLocation.Remote:
						client = new UntypedServiceClient(typeof(UntypedSignalrRelayProxy), proxyName);
						break;
				}

				var request = new AppRequestMessage(this.formDef.RequestClrType, this.formDef.ServiceClrType);
				request.Fields = ExtractInputValues();

				var response = Task.Run(async () => await client.Service.Execute(request)).Result;

				// todo: 06/12/18 gph. Non-standard field name. 
				// Standardize or 'ExtractOutputValues' after adding response schema to formDef.
				var responseText = default(string);
				switch (this.formDef.Name)
				{
					case "Calculator":
						responseText = response.Fields["Answer"];
						break;
					case "FileSystemQuery":
						responseText = response.Fields["ResponseString"];
						break;
				}

				this.resultLabel.Text = $"{selectedItem} service call submitted on {DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")}. Response {responseText}.";
			}
			catch (Exception ex)
			{
				var txt = ex.Message;
				if (ex.InnerException != null)
					txt += Environment.NewLine + ex.InnerException.Message;
				this.resultLabel.Text = txt;
			}
			finally
			{
			}
		}


		public View BuildPageContent()
		{
			this.Title = this.formDef.ContentTitle ?? this.formDef.Name;

			var view = new StackLayout();
			view.Children.Add(this.resetButton);

			// from formDef
			this.descHeader.Text = this.formDef.Description;

			view.Children.Add(this.descHeader);
			view.Children.Add(this.emptyHeader);
			view.Children.Add(this.serviceLocationPicker);
			view.Children.Add(this.emptyHeader);

			// from formDef fields
			AddInputFieldsFromFormDef(view, this.formDef.InputFields);

			view.Children.Add(this.submitButton);
			view.Children.Add(this.resultLabel);

			return view;
		}
		void AddInputFieldsFromFormDef(StackLayout view, IEnumerable<InputFieldDef> inputFields)
		{
			foreach (var field in inputFields)
			{
				if (field.Choices != null && field.Choices.Count > 0)
					AddPickerView(view, field);
				else
					AddEntryView(view, field);
			}
		}
		void SelectPickerItem(Picker picker, string itemText)
		{
			if (picker.Items == null || picker.Items.Count < 1)
				return;

			picker.SelectedIndex = 0;
			if (itemText == null)
				return;

			foreach (var item in picker.Items)
			{
				if (item.ToString().ToLower() == itemText.ToLower())
				{
					picker.SelectedItem = item;
					return;
				}
			}
		}
		void AddPickerView(StackLayout view, InputFieldDef field)
		{
			var picker = new Picker
			{
				StyleId = field.Name,
				ItemsSource = (IList)field.Choices,
				Title = field.Name,
				ClassId = field.DefaultValue,
			};
			SelectPickerItem(picker, field.DefaultValue);

			this.pickerControls.Add(picker);
			view.Children.Add(picker);
		}
		void AddEntryView(StackLayout view, InputFieldDef field)
		{
			view.Children.Add(new Label { Text = field.Caption ?? field.Name });
			var entry = new Entry
			{
				StyleId = field.Name,
				Text = field.DefaultValue,
				Placeholder = field.DefaultValue,
			};
			this.entryControls.Add(entry);
			view.Children.Add(entry);
		}
	}
}