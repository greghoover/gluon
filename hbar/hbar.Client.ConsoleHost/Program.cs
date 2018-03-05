using System;
using hbar.Client;

namespace hbar.Client.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client Console Host");

            while (true)
            {
                {
                    foreach (var item in Client.GetServices())
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
            var client = new Client();
            Console.Write("Enter folder path to check: ");
            var folderPath = Console.ReadLine().Trim();
            if (folderPath == string.Empty)
                return;

            var result = client.DoFileSystemQuery(folderPath);
            Console.WriteLine($"Was folder path [{folderPath}] found? [{result}].");
        }
    }
}
