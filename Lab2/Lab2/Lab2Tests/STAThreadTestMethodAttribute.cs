using System;
using System.Threading;

namespace Lab2Tests
{
    public class STAThreadTestMethodAttribute : TestMethodAttribute
    {
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            TestResult[] result = null;

            var thread = new Thread(() =>
            {
                result = base.Execute(testMethod);
            });

            thread.SetApartmentState(ApartmentState.STA); // Устанавливаем STA
            thread.Start();
            thread.Join(); // Ожидаем завершения потока

            return result;
        }
    }
}