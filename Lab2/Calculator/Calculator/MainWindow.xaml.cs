using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICalculatorView
    {
        private readonly ICalculatorPresenter presenter;

        public MainWindow()
        {
            InitializeComponent();
            var calculator = new Calculator();
            presenter = new CalculatorPresenter(calculator, this);
        }

        public string GetFirstArgumentAsString()
        {
            return FirstArgument.Text;
        }

        public string GetSecondArgumentAsString()
        {
            return SecondArgument.Text;
        }

        public void PrintResult(double result)
        {
            ResultBlock.Text = $"Результат: {result}";
            ErrorBlock.Text = "";
        }

        public void DisplayError(string message)
        {
            ErrorBlock.Text = $"Ошибка: {message}";
        }

        private void OnPlusClicked(object sender, RoutedEventArgs e)
        {
            presenter.OnPlusClicked();
        }

        private void OnMinusClicked(object sender, RoutedEventArgs e)
        {
            presenter.OnMinusClicked();
        }

        private void OnMultiplyClicked(object sender, RoutedEventArgs e)
        {
            presenter.OnMultiplyClicked();
        }

        private void OnDivideClicked(object sender, RoutedEventArgs e)
        {
            presenter.OnDivideClicked();
        }
    }
}