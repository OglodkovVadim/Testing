using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Calculator : ICalculator
    {
        public double Sum(double a, double b)
        {
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (Math.Abs(b) < 1e-8)
                throw new ArithmeticException("Деление на очень малое число близкое к нулю.");
            return a / b;
        }
    }
}
