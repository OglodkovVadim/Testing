using Moq;
using Lab2;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Lab2Tests
{
    [TestClass]
    public class MainWindowTests
    {
        private Mock<ICalculatorPresenter> presenterMock;
        private MainWindow mainWindow;

        [TestInitialize]
        public void Setup()
        {
            presenterMock = new Mock<ICalculatorPresenter>();

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                mainWindow = new MainWindow(presenterMock.Object);
                mainWindow.Show();
            });
        }

        [TestCleanup]
        public void Cleanup()
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                mainWindow.Close();
            });
        }

        [TestMethod]
        public void PlusButton_Clicked_CallsOnPlusClickedInPresenter()
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                var plusButton = mainWindow.FindName("PlusButton") as Button;
                Assert.IsNotNull(plusButton, "Кнопка '+' не найдена.");
                plusButton.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));
                presenterMock.Verify(p => p.OnPlusClicked(), Times.Once);
            });
        }

        [TestMethod]
        public void DisplayError_ShowsErrorMessageInView()
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                mainWindow.DisplayError("Test Error");
                var errorBlock = mainWindow.FindName("ErrorBlock") as TextBlock;
                Assert.IsNotNull(errorBlock, "Элемент ErrorBlock не найден.");
                Assert.AreEqual("Ошибка: Test Error", errorBlock.Text);
            });
        }
    }
}
