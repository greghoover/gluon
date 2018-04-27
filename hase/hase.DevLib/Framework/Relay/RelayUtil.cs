namespace hase.DevLib.Framework.Relay
{
    public enum RelayTypeEnum
    {
        Unknown = 0,
        SignalR = 1,
        NamedPipes = 2,
        ZeroMQ = 3,
    }
    public static class RelayUtil
    {
        public static RelayTypeEnum RelayTypeDflt { get; } = RelayTypeEnum.SignalR;
    }
}
