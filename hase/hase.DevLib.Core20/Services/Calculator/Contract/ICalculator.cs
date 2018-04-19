using System;
using System.Collections.Generic;
using System.Text;

namespace hase.DevLib.Services.Calculator.Contract
{
    public interface ICalculator
    {
        int Add(int i1, int i2);
        int Sub(int i1, int i2);
    }
}
