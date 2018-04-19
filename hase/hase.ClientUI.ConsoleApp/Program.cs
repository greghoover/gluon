using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.FileSystemQuery.Client;
using hase.DevLib.Services.FileSystemQuery.Contract;
using System;
using hase.DevLib.Framework.Relay.Signalr;

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

            var fsq = new FileSystemQuery(typeof(SignalrRelayProxyClient<FileSystemQueryRequest, FileSystemQueryResponse>));
            //var fsq = new FileSystemQuery(typeof(NamedPipeRelayProxyClient<FileSystemQueryRequest, FileSystemQueryResponse>));
            var result = fsq.DoesDirectoryExist(folderPath);
            Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
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

            var calc = new Calculator(typeof(NamedPipeRelayProxyClient<CalculatorRequest, CalculatorResponse>));
            //var calc = new Calculator(typeof(NamedPipeRelayProxyClient<CalculatorRequest, CalculatorResponse>));
            var result = calc.Add(i1, i2);
            Console.WriteLine($"[{i1} + {i2}] = [{result}].");
        }
    }
}


