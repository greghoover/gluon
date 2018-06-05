using hase.DevLib.Framework.Contract._;

namespace hase.DevLib.Services._.Calculator.Contract
{
    public interface ICalculator : IServiceClient<CalculatorRequest, CalculatorResponse>
    {
        int? Add(int i1, int i2);
        int? Sub(int i1, int i2);
    }
}
