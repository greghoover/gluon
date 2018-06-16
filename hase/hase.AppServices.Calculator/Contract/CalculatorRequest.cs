using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.AppServices.Calculator.Contract
{
    [ProtoContract]
    public class CalculatorRequest : AppRequestMessage
    {
        [ProtoMember(1)]
        public CalculatorOpEnum Operation { get; set; }
        [ProtoMember(2)]
        public int Number1 { get; set; }
        [ProtoMember(3)]
        public int Number2 { get; set; }

        public CalculatorRequest() { }
        public CalculatorRequest(CalculatorOpEnum op, int n1, int n2)
        {
            Operation = op;
            Number1 = n1;
            Number2 = n2;
        }

        public override string ToString() =>
             $"N1[{this.Number1}] Op[{this.Operation}] N2[{this.Number2}]";
    }
}
