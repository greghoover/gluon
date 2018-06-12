﻿using hase.DevLib.Framework.Service;
using hase.DevLib.Services.Calculator.Contract;
using System.Threading.Tasks;

namespace hase.DevLib.Services.Calculator.Service
{
    public class CalculatorService : ServiceBase<CalculatorRequest, CalculatorResponse>
    {
        // todo: consider making this an async method.
        public override Task<CalculatorResponse> Execute(CalculatorRequest request)
        {
            int result = 0;
            switch (request.Operation)
            {
                case CalculatorOpEnum.Add:
                    result = request.Number1 + request.Number2;
                    break;
                case CalculatorOpEnum.Subtract:
                    result = request.Number1 - request.Number2;
                    break;
            }

            var response = new CalculatorResponse(request, result);
            return Task.FromResult(response);
        }
    }
}
