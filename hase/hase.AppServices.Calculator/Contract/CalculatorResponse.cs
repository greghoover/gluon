using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.AppServices.Calculator.Contract
{
    [ProtoContract]
    public class CalculatorResponse : AppResponseMessage
    {
        [ProtoMember(1)]
        public int? Answer { get; set; }

        private CalculatorResponse() { }
        public CalculatorResponse(CalculatorRequest request, int? result)
        {
            this.Answer = result;
        }

        public override string ToString() =>
            $"Request: [{this.AppRequestMessage}] Response: [{this.Answer}]";
    }
}
