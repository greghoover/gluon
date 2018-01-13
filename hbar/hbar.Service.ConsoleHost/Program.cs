using System;
using hbar.Service;

namespace hbar.Service.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Service Dispatcher");
            var dispatcher = new ServiceDispatcher();
            dispatcher.Run();
        }
    }
}
