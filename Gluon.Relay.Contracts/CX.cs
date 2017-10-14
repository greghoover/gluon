namespace Gluon.Relay.Contracts
{
    public class CX
    {
        public static string RequestToClientMethodName { get { return "RequestToClientAsync"; } }
        public static string ResponseFromClientMethodName { get { return "ResponseFromClientAsync"; } }

        public static string WorkerMethodName { get { return "DoWork"; } }
    }
}
