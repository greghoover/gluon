namespace Gluon.Tester.Contracts
{
    public class RpcResponseMsg
    {
        public RpcRequestMsg Request { get; set; }
        public string ResponseString { get; set; }

        public RpcResponseMsg() : this(null, string.Empty) { }
        public RpcResponseMsg(RpcRequestMsg request) : this(request, string.Empty) { }
        public RpcResponseMsg(RpcRequestMsg request, string responseString)
        {
            this.Request = request;
            this.ResponseString = responseString;
        }

        public override string ToString()
        {
            var str = string.Empty;
            str += $"Request: {this.Request.RequestString} Response: {this.ResponseString}.";
            return str;
        }
    }
}
