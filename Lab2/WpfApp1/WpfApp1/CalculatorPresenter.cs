using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class CalculatorPresenter : ICalculatorPresenter
    {
        private readonly ICalculator calculator;
        private readonly ICalculatorView view;

        public CalculatorPresenter(ICalculator calculator, ICalculatorView view)
        {
            this.calculator = calculator;
            this.view = view;
        }

        public void OnPlusClicked()
        {
            TryPerformOperation(calculator.Sum);
        }

        public void OnMinusClicked()
        {
            TryPerformOperation(calculator.Subtract);
        }

        public void OnDivideClicked()
        {
            TryPerformOperation(calculator.Divide);
        }

        public void OnMultiplyClicked()
        {
            TryPerformOperation(calculator.Multiply);
        }

        private void TryPerformOperation(Func<double, double, double> operation)
        {
            try
            {
                double firstArg = double.Parse(view.GetFirstArgumentAsString());
                double secondArg = double.Parse(view.GetSecondArgumentAsString());
                double result = operation(firstArg, secondArg);
                view.PrintResult(result);
            }
            catch (Exception ex)
            {
                view.DisplayError(ex.Message);
            }
        }
    }
}
