using Microsoft.Extensions.Configuration;
using System;

namespace hase.DevLib.Framework.Contract
{
	public abstract class ConfigBase<TConfig>
	{
		public abstract string SectionName { get; }

		private IConfigurationRoot _configRoot { get; set; }
		public IConfigurationRoot GetConfigRoot()
		{
			if (_configRoot == null)
			{
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
			if (_configSection == null)
			{
				var root = GetConfigRoot();
				var iSection = root.GetSection(SectionName);
				var tSection = iSection.Get<TConfig>();
				_configSection = tSection;
			}
			return _configSection;
		}
	}
}
