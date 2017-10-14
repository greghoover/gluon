namespace Gluon.Relay.Contracts
{
    public interface IMessageExchangePattern { }

    public interface IRequestResponse<TRequestMsg, TResponseMsg> : IMessageExchangePattern
    {
        TResponseMsg RequestResponse(TRequestMsg request);
    }

    public interface IEmit<TEvent> : IMessageExchangePattern
    {
        void Emit(TEvent msg);
    }

    //public interface ISend<TSend>
    //{
    //    void Send(TSend msg);
    //}
    //public interface IReceive<TRecv>
    //{
    //    void Receive(TRecv msg);
    //}
    //public interface IRequest<TRequest, TResponse>
    //{
    //    TResponse Request(TRequest request);
    //}
    //public interface IRespond<TRequest, TResponse>
    //{
    //    TResponse Respond(TRequest request);
    //}
}
