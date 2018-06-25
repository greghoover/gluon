using Microsoft.Extensions.Configuration;
using System;

namespace hase.DevLib.Framework.Contract
{
	public abstract class ConfigBase<TConfig>
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

		private TConfig _configSection { get; set; }
		public TConfig GetConfigSection()
		{
			return GetConfigRoot().GetSection(this.SectionName).Get<TConfig>();
		}
		public TConfig GetConfigSection(string instanceId)
		{
			this.InstanceId = instanceId;
			return GetConfigSection();
		}
	}
}
