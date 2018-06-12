﻿using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Services.Calculator.Contract
{
    public interface ICalculator : IServiceClient<CalculatorRequest, CalculatorResponse>
    {
        int? Add(int n1, int n2);
        int? Subtract(int n1, int n2);
    }
}
