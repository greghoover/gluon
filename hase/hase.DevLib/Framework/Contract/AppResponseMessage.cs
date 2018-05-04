namespace hase.DevLib.Framework.Contract
{
    public class AppResponseMessage : AppMessage
    {
        public AppRequestMessage AppRequestMessage { get; set; }

        protected AppResponseMessage() { }
        public AppResponseMessage(AppRequestMessage request)
        {
            this.AppRequestMessage = request;
        }
    }
}
