using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Contract
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
	}
}
