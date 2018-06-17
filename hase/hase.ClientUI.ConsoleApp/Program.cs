using hase.AppServices.Calculator.Client;
using hase.AppServices.Calculator.Contract;
using hase.AppServices.FileSystemQuery.Client;
using hase.AppServices.FileSystemQuery.Contract;
using hase.DevLib.Framework.Client;
using hase.DevLib.Framework.Relay;
//using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Relay.Signalr;
using System;

namespace hase.ClientUI.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Client Console Host");

			while (true)
			{
				{
					foreach (var item in ServiceTypesUtil.GetServices())
					{
						Console.WriteLine($"{item.Id}. {item.Desc}");
					}
					Console.Write("Enter choice: ");
					var input = Console.ReadLine().Trim();
					if (input == string.Empty)
						break;

					ServiceTypesEnum choice;
					if (!Enum.TryParse<ServiceTypesEnum>(input, out choice))
						break;

					switch (choice)
					{
						case ServiceTypesEnum.FileSystemQuery:
							DoFileSystemQuery();
							break;
						case ServiceTypesEnum.Calculator:
							DoCalculator();
							break;
						default:
							Console.WriteLine("Unrecognized number. Please try again.");
							break;
					}
				}
			}
		}

		static void DoFileSystemQuery()
		{
			Console.Write("Enter folder path to check if exists: ");
			var folderPath = Console.ReadLine().Trim();
			if (folderPath == string.Empty)
				return;

				var fsq = default(IFileSystemQuery);
				if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
				{
					fsq = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
				}
				if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
				{
					//fsq = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
				}
				var result = fsq.DoesDirectoryExist(folderPath);
				Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
		}
		static void DoCalculator()
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
			if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
			{
				var proxyType = typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>);
				calc = new Calculator(proxyType);
			}
			if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
			{
				//calc = new Calculator(typeof(NamedPipeRelayProxy<CalculatorRequest, CalculatorResponse>));
			}
			var result = calc.Add(n1, n2);
			Console.WriteLine($"[{n1} + {n2}] = [{result}].");
		}
	}
}


