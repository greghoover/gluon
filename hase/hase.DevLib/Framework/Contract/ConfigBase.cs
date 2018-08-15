using hase.DevLib.Framework.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Xamarin.Essentials;

namespace hase.DevLib.Framework.Contract
{
	public abstract class ConfigBase<TConfig> : IConfigurationSection
		where TConfig : IConfigurationSection
	{
		public abstract string DefaultSectionName { get; }

		private string _instanceId = string.Empty; // never set to null;
		protected string InstanceId
		{
			get { return _instanceId; }
			set { _instanceId = (value ?? string.Empty).Trim(); }
		}
		public string SectionName
		{
			get
			{
				if (this.InstanceId.Length > 0)
					return $"{this.DefaultSectionName}--{this.InstanceId}";
				else
					return this.DefaultSectionName;
			}
		}

		private IConfigurationRoot _configRoot { get; set; }
		public IConfigurationRoot GetConfigRoot()
		{
			if (_configRoot == null)
			{
				try
				{
					// todo: 06/25/18 gph. Pass in a configuration Action parameter instead of hard-coding the buider.
					Console.WriteLine("Building Configuration");
					var cb = new ConfigurationBuilder();

					var fw = RuntimeInformation.FrameworkDescription;
					var os = RuntimeInformation.OSDescription;
					var osArch = RuntimeInformation.OSArchitecture;
					var procArch = RuntimeInformation.ProcessArchitecture;

					var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
					var isUwp = os == "Microsoft Windows"; // e.g. 'Microsoft Windows 10.0.17134' for Win10 desktop
					var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
					var isAndroid = RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID"));
					var isOsx = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
					var isIos = RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));


					var fileName = "appsettings.json";
					var fileDir = string.Empty; // The default for Windows. Override for Xamarin apps.
					try { fileDir = FileSystem.AppDataDirectory; } catch { }
					// for UWP:
					//fileDir = @"C:\Users\greg\AppData\Local\Packages\45067d15-d739-49ea-b463-0315aa7c99ff_z8snkv6en0h58\LocalState";
					// for Android:
					//fileDir = @"/data/user/0/com.companyname.hase.ClientUI.XFApp/files";
					// for iOS:
					//fileDir = @"???";
					var filePath = Path.Combine(fileDir, fileName);

					if (isWindows)
					{
						cb.AddJsonFile(filePath); // UWP allows to read from specific areas of the file system.
						//cb.AddCommandLine(args);
					}
					else if (isLinux || isAndroid)
					{
						//var dict = new Dictionary<string, string>
						//{
						//	{"ServiceProxy:ProxyTypeName", "SignalrRelayProxy"},
						//	{"ServiceProxy:ProxyConfigSection", "SignalrRelayProxy"},
						//	{"ServiceProxy:ServiceTypeNames", "FileSystemQuery"},
						//	{"SignalrRelayProxy:HubUrl", "http://172.27.211.17:5000/route"},
						//};
						//cb.AddInMemoryCollection(dict);

						cb.AddJsonFile(new AltFileProvider(), filePath, optional: false, reloadOnChange: false);
					}
					else //if (isOsx || isIos)
					{
						//var dict = new Dictionary<string, string>
						//{
						//	{"ServiceProxy:ProxyTypeName", "SignalrRelayProxy"},
						//	{"ServiceProxy:ProxyConfigSection", "SignalrRelayProxy"},
						//	{"ServiceProxy:ServiceTypeNames", "FileSystemQuery"},
						//	{"SignalrRelayProxy:HubUrl", "http://192.168.1.125:5000/route"},
						//};
						//cb.AddInMemoryCollection(dict);

						cb.AddJsonFile(new AltFileProvider(), filePath, optional: false, reloadOnChange: false);
					}
					_configRoot = cb.Build();
				}
				catch (Exception ex)
				{
					var e = ex;
				}
			}
			return _configRoot;
		}

		public TConfig GetConfigSection()
		{
			return GetConfigRoot().GetSection(this.SectionName).Get<TConfig>();
		}
		public TConfig GetConfigSection(string instanceId)
		{
			this.InstanceId = instanceId;
			return GetConfigSection();
		}

		// Not actually implemented... just satisfies requirement of the interface.
		// Allows us to pass types derived from ConfigBase as IConfigurationSection.
		// After passing into the callee, can check to see if it implements the derived type, 
		// and skip mapping from IConfigurationSection if so.
		#region IConfigurationSection
		private TConfig _configSection { get; set; }

		string IConfigurationSection.Key => throw new NotImplementedException();

		string IConfigurationSection.Path => throw new NotImplementedException();

		string IConfigurationSection.Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		string IConfiguration.this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		IConfigurationSection IConfiguration.GetSection(string key)
		{
			throw new NotImplementedException();
		}

		IEnumerable<IConfigurationSection> IConfiguration.GetChildren()
		{
			throw new NotImplementedException();
		}

		IChangeToken IConfiguration.GetReloadToken()
		{
			throw new NotImplementedException();
		}
		#endregion IConfigurationSection
	}
}
