using hase.DevLib.Service;
using System;

namespace hase.ServiceApp.ConsoleHost
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
