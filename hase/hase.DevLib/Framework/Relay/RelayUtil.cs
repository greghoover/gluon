using System.Net;

namespace hase.DevLib.Framework.Relay
{
    public enum RelayTypeEnum
    {
        Unknown = 0,
        SignalR = 1,
        NamedPipes = 2,
        NetMq = 3,
    }
    public static class RelayUtil
    {
        public static RelayTypeEnum RelayTypeDflt { get; } = RelayTypeEnum.SignalR;

        public static string RelayHostName
        {
            get
            {
                //return Dns.GetHostName();
                return "DESKTOP-DAQURQ8";
            }
        }
        public static IPAddress[] RelayListenerIPs
        {
            get
            {
                //var addys = Dns.GetHostAddresses(RelayHostName);
                //return addys;
                return new IPAddress[] { IPAddress.Parse("192.168.1.17"), IPAddress.Parse("172.27.211.17"), IPAddress.Parse("172.18.112.1") };
            }
        }
        public static IPAddress[] RelayIPs
        {
            get
            {
                //var addys = Dns.GetHostAddresses(RelayHostName);
                //return addys;
                return new IPAddress[] { IPAddress.Parse("192.168.1.17"), IPAddress.Parse("10.0.2.2"), IPAddress.Parse("172.27.211.17"), IPAddress.Parse("172.18.112.1") };
            }
        }
    }
}
