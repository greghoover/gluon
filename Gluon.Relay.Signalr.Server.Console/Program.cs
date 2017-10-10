namespace Gluon.Relay.Signalr.Server.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting Gluon SignalR Relay Server...");
            Startup.BuildAndRunWebHost(args);
        }
    }
}
