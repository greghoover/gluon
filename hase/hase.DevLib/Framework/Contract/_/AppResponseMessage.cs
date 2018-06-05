namespace hase.DevLib.Framework.Contract._
{
    public class AppResponseMessage : AppMessage
    {
        public AppRequestMessage AppRequestMessage { get; set; }

        protected AppResponseMessage() { }
        public AppResponseMessage(AppRequestMessage request)
        {
            this.AppRequestMessage = request;
        }

        public override string ToString()
        {
            return $"ResponseId[{this.Headers.MessageId}]";
        }
    }
}
