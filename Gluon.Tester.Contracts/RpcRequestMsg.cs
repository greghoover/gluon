using Gluon.Relay.Contracts;
using System;

namespace Gluon.Tester.Contracts
{
    public class RpcRequestMsg : RelayMessageBase
    {
        public string RequestString { get; set; }
        public DateTimeOffset RequestedOn { get; set; }

        public RpcRequestMsg() : this(string.Empty) { }
        public RpcRequestMsg(string requestString)
        {
            this.RequestString = requestString;
            this.RequestedOn = DateTimeOffset.Now;
        }

        public override string ToString()
        {
            var str = string.Empty;
            str += $"Request: {this.RequestString} DateTime: {this.RequestedOn}.";
            return str;
        }
    }
}
