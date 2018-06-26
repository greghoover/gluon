using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

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
				// todo: 06/25/18 gph. Pass in a configuration Action parameter instead of hard-coding the buider.
				Console.WriteLine("Building Configuration");
				var cb = new ConfigurationBuilder();
				_configRoot = cb.AddJsonFile("appsettings.json")
					//.AddCommandLine(args)
					.Build();
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
