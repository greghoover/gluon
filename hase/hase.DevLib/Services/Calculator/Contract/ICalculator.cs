using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Services.Calculator.Contract
{
    public interface ICalculator : IServiceClient<CalculatorRequest, CalculatorResponse>
    {
        int? Add(int i1, int i2);
        int? Sub(int i1, int i2);
    }
}
