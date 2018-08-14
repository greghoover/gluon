using hase.DevLib.Framework.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
			//Debugger.Break();
			if (_configRoot == null)
			{
				try
				{
					// todo: 06/25/18 gph. Pass in a configuration Action parameter instead of hard-coding the buider.
					Console.WriteLine("Building Configuration");
					var cb = new ConfigurationBuilder();
					var cd = Directory.GetCurrentDirectory();
					//cb.SetBasePath(cd);

					var osDesc = RuntimeInformation.OSDescription;
					var fwDesc = RuntimeInformation.FrameworkDescription;
					var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
					var isOsx = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
					var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
					var isIos = RuntimeInformation.IsOSPlatform(OSPlatform.Create("IOS"));
					var isAndroid = RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID"));

					var fn = "appsettings.json";

					if (isWindows)
					{
						var asm = Assembly.GetEntryAssembly();
						var fileDir = string.Empty;
						if (asm.FullName.Contains(".UWP"))
							fileDir = @"C:\Users\greg\AppData\Local\Packages\45067d15-d739-49ea-b463-0315aa7c99ff_z8snkv6en0h58\LocalState"; // FileSystem.AppDataDirectory;
						var filePath = Path.Combine(fileDir, fn);

						//cb.AddJsonFile("appsettings.json");
						cb.AddJsonFile(filePath);
						//cb.AddCommandLine(args);
						_configRoot = cb.Build();
						//var subs = Directory.GetDirectories(cd);
					}
					else if (isLinux || isAndroid)
					{
						var fileDir = @"/data/user/0/com.companyname.hase.ClientUI.XFApp/files"; // FileSystem.AppDataDirectory;
						var filePath = Path.Combine(fileDir, fn);

						// todo: 06/28/18 gph. Read configuration from device.
						//var dict = new Dictionary<string, string>
						//{
						//	{"ServiceProxy:ProxyTypeName", "SignalrRelayProxy"},
						//	{"ServiceProxy:ProxyConfigSection", "SignalrRelayProxy"},
						//	{"ServiceProxy:ServiceTypeNames", "FileSystemQuery"},
						//	{"SignalrRelayProxy:HubUrl", "http://172.27.211.17:5000/route"},
						//};
						//cb.AddInMemoryCollection(dict);

						cb.AddJsonFile(new AltFileProvider(), filePath, optional: false, reloadOnChange: false);
						_configRoot = cb.Build();
					}
					else //if (isOsx || isIos)
					{
						var fileDir = "uh, not sure yet"; // FileSystem.AppDataDirectory;
						var filePath = Path.Combine(fileDir, fn);

						// todo: 07/06/18 gph. Read configuration from device.
						//                  var dict = new Dictionary<string, string>
						//{
						//	{"ServiceProxy:ProxyTypeName", "SignalrRelayProxy"},
						//	{"ServiceProxy:ProxyConfigSection", "SignalrRelayProxy"},
						//	{"ServiceProxy:ServiceTypeNames", "FileSystemQuery"},
						//	{"SignalrRelayProxy:HubUrl", "http://192.168.1.125:5000/route"},
						//};
						//cb.AddInMemoryCollection(dict);

						cb.AddJsonFile(new AltFileProvider(), filePath, optional: false, reloadOnChange: false);
						_configRoot = cb.Build();
					}
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
