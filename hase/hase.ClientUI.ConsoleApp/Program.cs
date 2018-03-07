﻿using System;
using hase.DevLib.Client;
using hase.DevLib.Client.FileSystemQuery;

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
                    foreach (var item in ClientUtil.GetServices())
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
            Console.Write("Enter folder path to check: ");
            var folderPath = Console.ReadLine().Trim();
            if (folderPath == string.Empty)
                return;

            var client = new FileSystemQueryClient(isRemote: false);
            var result = client.DoFileSystemQuery(folderPath);
            Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
        }
    }
}


