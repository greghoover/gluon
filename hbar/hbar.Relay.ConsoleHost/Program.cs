using hbar.Relay;
using System;

namespace hbar.Relay.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Named Pipe Relay.");
            var relay = new NamedPipeRelay();
            relay.Start();
            Console.WriteLine("Named Pipe Relay started.");
        }
    }
}
