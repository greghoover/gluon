using hase.AppServices.Calculator.Client;
using hase.AppServices.Calculator.Contract;
using hase.AppServices.FileSystemQuery.Client;
using hase.AppServices.FileSystemQuery.Contract;
using hase.DevLib.Framework.Relay.Proxy;
//using hase.DevLib.Framework.Relay.NamedPipe;
using hase.Relays.Signalr.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace hase.ClientUI.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Client Console Host");

			Console.WriteLine("Getting Configuration");
			var hostCfg = new RelayProxyConfig().GetConfigSection();
			var proxyCfg = hostCfg.GetConfigRoot().GetSection(hostCfg.ProxyConfigSection);

			while (true)
			{
				{
					var serviceTypes = hostCfg.ServiceTypeNames;
					var options = new Dictionary<string, string>();
					var i = 0;
					foreach (var svcTyp in serviceTypes)
					{
						i++;
						options.Add(i.ToString(), svcTyp);
						Console.WriteLine($"{i}. {svcTyp}");
					}
					Console.Write("Enter choice: ");
					var input = Console.ReadLine().Trim();
					if (input.ToLower() == "x")
						break;
					if (!options.ContainsKey(input))
						continue;

					var choice = options[input];
					switch (choice)
					{
						case nameof(FileSystemQuery):
							DoFileSystemQuery(hostCfg, proxyCfg);
							break;
						case nameof(Calculator):
							DoCalculator(hostCfg, proxyCfg);
							break;
						default:
							Console.WriteLine("Unrecognized number. Please try again.");
							break;
					}
				}
			}
		}

		static void DoFileSystemQuery(RelayProxyConfig hostCfg, IConfigurationSection proxyCfg)
		{
			Console.Write("Enter folder path to check if exists: ");
			var folderPath = Console.ReadLine().Trim();
			if (folderPath == string.Empty)
				return;

			var fsq = default(IFileSystemQuery);
			switch (hostCfg.ProxyTypeName)
			{
				case nameof(SignalrRelayProxy):
					var proxyType = typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>);
					fsq = new FileSystemQuery(proxyType, proxyCfg);
					break;
				//case named pipe...
				//	break;
				default:
					throw new NotSupportedException($"{hostCfg.ProxyTypeName} relay client is currently not supported.");
			}

			var result = fsq.DoesDirectoryExist(folderPath);
			Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
		}
		static void DoCalculator(RelayProxyConfig hostCfg, IConfigurationSection proxyCfg)
		{
			Console.WriteLine("For temporary simplicity, will always perform add.");
			Console.Write("Enter Number1: ");
			var input1 = Console.ReadLine().Trim();
			if (input1 == string.Empty)
				return;
			int n1;
			if (!int.TryParse(input1, out n1))
				return;

			Console.Write("Enter Number2: ");
			var input2 = Console.ReadLine().Trim();
			if (input2 == string.Empty)
				return;
			int n2;
			if (!int.TryParse(input2, out n2))
				return;

			var calc = default(ICalculator);
			switch (hostCfg.ProxyTypeName)
			{
				case nameof(SignalrRelayProxy):
					var proxyType = typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>);
					calc = new Calculator(proxyType, proxyCfg);
					break;
				//case named pipe...
				//	break;
				default:
					throw new NotSupportedException($"{hostCfg.ProxyTypeName} relay client is currently not supported.");
			}
			var result = calc.Add(n1, n2);
			Console.WriteLine($"[{n1} + {n2}] = [{result}].");
		}
	}
}


