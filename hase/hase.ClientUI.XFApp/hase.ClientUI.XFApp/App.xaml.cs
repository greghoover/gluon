using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Microsoft.Extensions.Configuration;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace hase.ClientUI.XFApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			// Android + UWP
			var p26 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var p35 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			var p00 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			var p16 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			var p28 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			var p05a = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var p13 = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			var p39 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			var p14 = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
			var p05b = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var p21 = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
			var p40 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

			// UWP Only
			var p25 = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
			var p46 = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
			var p53 = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic);
			var p54 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
			var p55 = Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos);
			var p33 = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
			var p06 = Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
			var p34 = Environment.GetFolderPath(Environment.SpecialFolder.History);
			var p32 = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
			var p08 = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
			var p37 = Environment.GetFolderPath(Environment.SpecialFolder.System);
			var p41 = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
			var p36 = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

			// Android Only
			var p45 = Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates);
			var p20 = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

			// Not Android and Not UWP
			var p48 = Environment.GetFolderPath(Environment.SpecialFolder.AdminTools);
			var p59 = Environment.GetFolderPath(Environment.SpecialFolder.CDBurning);
			var p47 = Environment.GetFolderPath(Environment.SpecialFolder.CommonAdminTools);
			var p58 = Environment.GetFolderPath(Environment.SpecialFolder.CommonOemLinks);
			var p43 = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
			var p44 = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86);
			var p23 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);
			var p22 = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
			var p24 = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
			var p57 = Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources);
			var p17 = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			var p19 = Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts);
			var p27 = Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts);
			var p38 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			var p42 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			var p02 = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
			var p56 = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
			var p09 = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
			var p11 = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
			var p07 = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

			//var content = "file content";
			//var fileName = "fileName.txt";
			//var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileName);

			//var cacheDir = FileSystem.CacheDirectory;
			//var appDataDir = FileSystem.AppDataDirectory;
			//try
			//{
			//	var cacheFiles = Directory.GetFiles(cacheDir);
			//	var appDataFiles = Directory.GetFiles(appDataDir);

			//	File.WriteAllText(Path.Combine(cacheDir, fileName), content);
			//	var c1 = File.ReadAllText(Path.Combine(cacheDir, fileName));
			//	File.WriteAllText(Path.Combine(appDataDir, fileName), content);
			//	var c2 = File.ReadAllText(Path.Combine(appDataDir, fileName));
			//}
			//catch (Exception ex) { }

			try
			{
				var fn = "appsettings.json";
				using (var stream = FileSystem.OpenAppPackageFileAsync(fn).Result)
				{
					var textContent = default(string);
					//using (var writer = new StreamWriter(stream))
					//{
					//    writer.WriteLine("mycontent");
					//}
					using (var reader = new StreamReader(stream))
					{
						textContent = reader.ReadToEndAsync().Result;
					}

					//var writeDir = FileSystem.CacheDirectory;
					var writeDir = FileSystem.AppDataDirectory;
					var filePath = Path.Combine(writeDir, fn);

					File.WriteAllText(filePath, textContent);
					var proveIt = File.ReadAllText(filePath);
				}
			}
			catch (Exception ex) { }

			//try
			//{
			//    File.WriteAllText(path, content);
			//    var fil = File.ReadAllText(path);
			//}
			//catch (Exception ex) { }

			//try
			//{
			//	string text = "";
			//	var assembly = Assembly.GetEntryAssembly();
			//	//var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
			//	using (var stream = assembly.GetManifestResourceStream("fred.txt"))
			//	using (var reader = new StreamReader(stream))
			//	{
			//		text = reader.ReadToEnd();
			//	}
			//}
			//catch (Exception ex) { }

			//MainPage = new ServiceClientsTabbedPage();
			MainPage = new GenericServiceClientTabbedPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
