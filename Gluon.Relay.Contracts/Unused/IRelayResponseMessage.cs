namespace Gluon.Relay.Contracts.Unused
{
    public interface IRelayResponseMessage : IRelayMessage
    {
        IRelayRequestMessage Request { get; set; }
    }
}
