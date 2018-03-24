using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.FileSystemQuery.Client;
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
                    var choice = Console.ReadLine().Trim();
                    if (choice == string.Empty)
                        break;

                    switch (choice)
                    {
                        case "1":
                            DoFileSystemQuery();
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

            var fsq = FileSystemQuery.NewWithNamedPipeProxy();
            var result = fsq.DoesDirectoryExist(folderPath);
            Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
        }
    }
}


