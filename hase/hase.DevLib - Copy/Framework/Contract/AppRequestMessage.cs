namespace hase.DevLib.Framework.Contract
{
    public class AppRequestMessage : AppMessage
    {
        public override string ToString()
        {
            return $"RequestId[{this.Headers.MessageId}]";
        }
    }
}
