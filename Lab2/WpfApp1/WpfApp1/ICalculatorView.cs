using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public interface ICalculatorView
    {
        /// <summary>
        /// Отображает результат вычисления
        /// </summary>
        void PrintResult(double result);

        /// <summary>
        /// Показывает ошибку, например деление на 0, пустые аргументы и прочее
        /// </summary>
        void DisplayError(string message);

        /// <summary>
        /// Возвращает значение, введенное в поле первого аргумента
        /// </summary>
        string GetFirstArgumentAsString();

        /// <summary>
        /// Возвращает значение, введенное в поле второго аргумента
        /// </summary>
        string GetSecondArgumentAsString();
    }
}
