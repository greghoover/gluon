using hase.DevLib.Framework.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
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

					cb.AddJsonFile(new AltFileProvider(), filePath, optional: false, reloadOnChange: false);
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
