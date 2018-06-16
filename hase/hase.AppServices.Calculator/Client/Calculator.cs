using hase.DevLib.Framework.Client;
using hase.AppServices.Calculator.Contract;
using System;
using System.Threading.Tasks;

namespace hase.AppServices.Calculator.Client
{
    public class Calculator : ServiceClientBase<CalculatorRequest, CalculatorResponse>, ICalculator
    {
        /// <summary>
        /// Create local service instance.
        /// </summary>
        public Calculator() { }
        /// <summary>
        /// Create proxied service instance.
        /// </summary>
        public Calculator(Type proxyType) : base(proxyType) { }

        public int? Add(int n1, int n2)
        {
            var request = new CalculatorRequest
            {
                Operation = CalculatorOpEnum.Add,
                Number1 = n1,
                Number2 = n2,
            };

            var response = Task.Run(async () => await this.Service.Execute(request)).Result;
            return response.Answer;
        }

        public int? Subtract(int n1, int n2)
        {
            var request = new CalculatorRequest
            {
                Operation = CalculatorOpEnum.Subtract,
                Number1 = n1,
                Number2 = n2,
            };

            var response = Task.Run<CalculatorResponse>(() => Service.Execute(request)).Result;
            return response.Answer;
        }
    }
}
