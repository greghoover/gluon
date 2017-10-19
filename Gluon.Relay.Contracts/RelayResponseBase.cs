namespace Gluon.Relay.Contracts
{
    public abstract class RelayResponseBase<TRqst> : RelayMessageBase where TRqst : RelayMessageBase
    {
        public TRqst Request { get; set; }

        public RelayResponseBase(TRqst request) : base()
        {
            if (request != null)
            {
                this.CorrelationId = request.CorrelationId;
                this.Request = request;
            }
        }
    }
}
