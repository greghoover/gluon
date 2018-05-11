using hase.DevLib.Framework.Contract;
using hase.DevLib.Services.Calculator.Service;

namespace hase.DevLib.Services.Calculator.Contract
{
    public interface ICalculator : IServiceClient<CalculatorService, CalculatorRequest, CalculatorResponse>
    {
        int? Add(int i1, int i2);
        int? Sub(int i1, int i2);
    }
}
