using System;
using hase.DevLib.Relay;

namespace hase.RelayHub.ConsoleHost
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
