using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WpfCalculator
{
    [TestClass]
    public class CalculatorPresenterTests
    {
        private Mock<ICalculatorView> viewMock;
        private CalculatorPresenter presenter;
        private ICalculator calculator;

        [TestInitialize]
        public void Setup()
        {
            viewMock = new Mock<ICalculatorView>();
            calculator = new WpfCalculator.Calculator();
            presenter = new CalculatorPresenter(calculator, viewMock.Object);
        }

        [TestMethod]
        public void OnPlusClicked_ValidInputs_DisplaysCorrectResult()
        {
            // Arrange
            viewMock.Setup(v => v.GetFirstArgumentAsString()).Returns("5");
            viewMock.Setup(v => v.GetSecondArgumentAsString()).Returns("3");

            // Act
            presenter.OnPlusClicked();

            // Assert
            viewMock.Verify(v => v.PrintResult(8), Times.Once);
        }

        [TestMethod]
        public void OnMinusClicked_ValidInputs_DisplaysCorrectResult()
        {
            viewMock.Setup(v => v.GetFirstArgumentAsString()).Returns("10");
            viewMock.Setup(v => v.GetSecondArgumentAsString()).Returns("4");

            presenter.OnMinusClicked();

            viewMock.Verify(v => v.PrintResult(6), Times.Once);
        }

        [TestMethod]
        public void OnMultiplyClicked_ValidInputs_DisplaysCorrectResult()
        {
            viewMock.Setup(v => v.GetFirstArgumentAsString()).Returns("2");
            viewMock.Setup(v => v.GetSecondArgumentAsString()).Returns("3");

            presenter.OnMultiplyClicked();

            viewMock.Verify(v => v.PrintResult(6), Times.Once);
        }

        [TestMethod]
        public void OnDivideClicked_ValidInputs_DisplaysCorrectResult()
        {
            viewMock.Setup(v => v.GetFirstArgumentAsString()).Returns("10");
            viewMock.Setup(v => v.GetSecondArgumentAsString()).Returns("2");

            presenter.OnDivideClicked();

            viewMock.Verify(v => v.PrintResult(5), Times.Once);
        }

        [TestMethod]
        public void OnDivideClicked_DivideByZero_DisplaysError()
        {
            viewMock.Setup(v => v.GetFirstArgumentAsString()).Returns("10");
            viewMock.Setup(v => v.GetSecondArgumentAsString()).Returns("0");

            presenter.OnDivideClicked();

            viewMock.Verify(v => v.DisplayError("Деление на очень малое число близкое к нулю."), Times.Once);
        }
    }
}