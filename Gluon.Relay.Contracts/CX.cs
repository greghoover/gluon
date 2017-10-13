namespace Gluon.Relay.Contracts
{
    public class CX
    {
        public static string PushToClientsMethodName { get { return "PushToClientsAsync"; } }

        public static string RpcToClientMethodName { get { return "RpcToClientAsync"; } }
        public static string RpcFromClientMethodName { get { return "RpcFromClientAsync"; } }

        public static string WorkerMethodName { get { return "DoWork"; } }
    }
}
