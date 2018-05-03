using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.DevLib.Services.Calculator.Contract
{
    [ProtoContract]
    public class CalculatorResponse : ApplicationResponseMessage
    {
        [ProtoMember(1)]
        public CalculatorRequest Request
        {
            get { return this.ApplicationRequestMessage as CalculatorRequest; }
        }
        [ProtoMember(2)]
        public int? Result { get; set; }

        private CalculatorResponse() { }
        public CalculatorResponse(CalculatorRequest request, int? result)
        {
            //this.ApplicationRequestMessage = request;
            this.Result = result;
        }

        public override string ToString() =>
            $"Request: [{this.Request}] Response: [{this.Result}]";
    }
}
