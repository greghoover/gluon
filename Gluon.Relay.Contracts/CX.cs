namespace Gluon.Relay.Contracts
{
    public class CX
    {
        public static string WorkerMethodName { get { return "DoWork"; } }

        //public static string PushToClientMethodName { get { return "PushToClientAsync"; } }
        public static string PushToClientsMethodName { get { return "PushToClientsAsync"; } }
    }
}
