using hase.DevLib.Framework.Contract;
using ProtoBuf;

namespace hase.DevLib.Services.Calculator.Contract
{
    [ProtoContract]
    public class CalculatorRequest : AppRequestMessage
    {
        [ProtoMember(1)]
        public CalculatorOpEnum Operation { get; set; }
        [ProtoMember(2)]
        public int I1 { get; set; }
        [ProtoMember(3)]
        public int I2 { get; set; }

        public CalculatorRequest() { }
        public CalculatorRequest(CalculatorOpEnum op, int i1, int i2)
        {
            Operation = op;
            I1 = i1;
            I2 = i2;
        }

        public override string ToString() =>
             $"I1[{this.I1}] Op[{this.Operation}] I2[{this.I2}]";
    }
}
