using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator
{
    public interface ICalculator
    {
        /// <summary>
        /// Вычисляет сумму двух чисел
        /// </summary>
        double Sum(double a, double b);

        /// <summary>
        /// Вычисляет разность двух чисел a - b
        /// </summary>
        double Subtract(double a, double b);

        /// <summary>
        /// Вычисляет произведение двух чисел
        /// </summary>
        double Multiply(double a, double b);

        /// <summary>
        /// Вычисляет отношение числа а к числу b. Должен выбросить 
        /// ArithmeticException если |b| < 10e-8
        /// </summary>
        double Divide(double a, double b);
    }

}
