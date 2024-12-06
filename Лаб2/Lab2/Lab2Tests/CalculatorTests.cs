using Moq;
using Lab2;

namespace Lab2Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private ICalculator calculator;

        [TestInitialize]
        public void Setup()
        {
            calculator = new Calculator();
        }

        [TestMethod]
        public void Sum_TwoNumbers_ReturnsCorrectResult()
        {
            double result = calculator.Sum(3, 4);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Subtract_TwoNumbers_ReturnsCorrectResult()
        {
            double result = calculator.Subtract(10, 4);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Multiply_TwoNumbers_ReturnsCorrectResult()
        {
            double result = calculator.Multiply(2, 5);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Divide_TwoNumbers_ReturnsCorrectResult()
        {
            double result = calculator.Divide(10, 2);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void Divide_ByZero_ThrowsArithmeticException()
        {
            calculator.Divide(10, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void Divide_ByVerySmallNumber_ThrowsArithmeticException()
        {
            calculator.Divide(10, 1e-9);
        }
    }
}
