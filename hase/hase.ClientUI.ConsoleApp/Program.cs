using hase.DevLib.Framework.Client._;
using hase.DevLib.Framework.Contract._;
using hase.DevLib.Framework.Relay;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Framework.Relay.Signalr;
using signalr = hase.DevLib.Framework.Relay._.Signalr;
using hase.DevLib.Framework.Service;
using hase.DevLib.Services;
using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using calcCli = hase.DevLib.Services._.Calculator.Client;
using calcCon = hase.DevLib.Services._.Calculator.Contract;
using calcS = hase.DevLib.Services._.Calculator.Service;
using fileCli = hase.DevLib.Services._.FileSystemQuery.Client;
using fileCon = hase.DevLib.Services._.FileSystemQuery.Contract;
using fileS = hase.DevLib.Services._.FileSystemQuery.Service;
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

            if (RelayUtil.DispatchStrategy == DispatchStrategyEnum.Generics)
            {
                var fsq = default(IFileSystemQuery);
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
                {
                    fsq = new FileSystemQuery(typeof(SignalrRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
                }
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
                {
                    fsq = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
                }
                var result = fsq.DoesDirectoryExist(folderPath);
                Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
            }
            else if (RelayUtil.DispatchStrategy == DispatchStrategyEnum.Reflection)
            {
                var fsq = default(fileCon.IFileSystemQuery);
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
                {
                    fsq = new fileCli.FileSystemQuery(typeof(signalr.SignalrRelayProxy<fileCon.FileSystemQueryRequest, fileCon.FileSystemQueryResponse>));
                }
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
                {
                    //fsq = new FileSystemQuery(typeof(NamedPipeRelayProxy<FileSystemQueryRequest, FileSystemQueryResponse>));
                }
                var result = fsq.DoesDirectoryExist(folderPath);
                Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
            }
        }
        static void DoCalculator()
        {
            Console.WriteLine("For temporary simplicity, will always perform add.");
            Console.Write("Enter I1: ");
            var input1 = Console.ReadLine().Trim();
            if (input1 == string.Empty)
                return;
            int i1;
            if (!int.TryParse(input1, out i1))
                return;

            Console.Write("Enter I2: ");
            var input2 = Console.ReadLine().Trim();
            if (input2 == string.Empty)
                return;
            int i2;
            if (!int.TryParse(input2, out i2))
                return;

            if (RelayUtil.DispatchStrategy == DispatchStrategyEnum.Generics)
            {
                var calc = default(ICalculator);
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
                {
                    calc = new Calculator(typeof(SignalrRelayProxy<CalculatorRequest, CalculatorResponse>));
                }
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
                {
                    calc = new Calculator(typeof(NamedPipeRelayProxy<CalculatorRequest, CalculatorResponse>));
                }
                var result = calc.Add(i1, i2);
                Console.WriteLine($"[{i1} + {i2}] = [{result}].");
            }
            else if (RelayUtil.DispatchStrategy == DispatchStrategyEnum.Reflection)
            {
                var calc = default(calcCon.ICalculator);
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.SignalR)
                {
                    var proxyType = typeof(signalr.SignalrRelayProxy<calcCon.CalculatorRequest, calcCon.CalculatorResponse>);
                    calc = new calcCli.Calculator(proxyType);
                }
                if (RelayUtil.RelayTypeDflt == RelayTypeEnum.NamedPipes)
                {
                    //calc = new Calculator(typeof(NamedPipeRelayProxy<CalculatorRequest, CalculatorResponse>));
                }
                var result = calc.Add(i1, i2);
                Console.WriteLine($"[{i1} + {i2}] = [{result}].");
            }
        }
    }
}


