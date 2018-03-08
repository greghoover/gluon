using System;
using hase.DevLib.Relay.NamedPipe;

namespace hase.RelayHub.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Named Pipe Relay.");
            var relay = new NamedPipeRelayHub();
            relay.Start();
            Console.WriteLine("Named Pipe Relay started.");
        }
    }
}
