using System.Threading.Tasks;

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
                return "localhost";
            }
        }

        public static async Task<TBase> GeneralizeTask<TBase, TDerived>(Task<TDerived> task)
            where TDerived : TBase
        {
            return (TBase)await task;
        }
    }
}
