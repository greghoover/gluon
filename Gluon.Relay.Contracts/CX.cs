namespace Gluon.Relay.Contracts
{
    public class CX
    {
        public static string RelayRequestMethodName { get { return "RelayRequestAsync"; } }
        public static string RelayResponseMethodName { get { return "RelayResponseAsync"; } }

        public static string RelayRequestGroupMethodName { get { return "RelayRequestGroupAsync"; } }

        public static string WorkerMethodName { get { return "DoWork"; } }

        //public static string AuthorizationKey { get { return "Authorization"; } }
    }
}
