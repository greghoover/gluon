namespace hase.DevLib.Framework.Contract
{
    public class ApplicationResponseMessage : ApplicationMessage
    {
        public ApplicationRequestMessage ApplicationRequestMessage { get; set; }

        public ApplicationResponseMessage() { }
        public ApplicationResponseMessage(ApplicationRequestMessage request)
        {
            this.ApplicationRequestMessage = request;
        }
    }
}
