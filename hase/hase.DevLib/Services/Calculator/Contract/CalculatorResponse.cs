using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.DevLib.Services.Calculator.Contract
{
    [ProtoContract]
    public class CalculatorResponse : AppResponseMessage
    {
        [ProtoMember(1)]
        public int? Result { get; set; }

        private CalculatorResponse() { }
        public CalculatorResponse(CalculatorRequest request, int? result)
        {
            this.Result = result;
        }

        public override string ToString() =>
            $"Request: [{this.AppRequestMessage}] Response: [{this.Result}]";
    }
}
