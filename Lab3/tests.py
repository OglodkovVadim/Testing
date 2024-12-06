import unittest
from unittest.mock import MagicMock, patch

from main import Calculator
from main import CalculatorPresenter
from main import CalculatorView

class TestCalculator(unittest.TestCase):
    def setUp(self):
        self.calculator = Calculator()

    def test_sum(self):
        self.assertEqual(self.calculator.sum(5, 3), 8)

    def test_subtract(self):
        self.assertEqual(self.calculator.subtract(10, 4), 6)

    def test_multiply(self):
        self.assertEqual(self.calculator.multiply(7, 3), 21)

    def test_divide(self):
        self.assertEqual(self.calculator.divide(10, 2), 5)

    def test_divide_by_zero(self):
        with self.assertRaises(ArithmeticError):
            self.calculator.divide(10, 0)


class TestCalculatorPresenter(unittest.TestCase):
    def setUp(self):
        self.mock_view = MagicMock(spec=CalculatorView)
        self.calculator = Calculator()
        self.presenter = CalculatorPresenter(self.mock_view, self.calculator)

    def test_on_plus_clicked(self):
        self.mock_view.get_first_argument_as_string.return_value = "3"
        self.mock_view.get_second_argument_as_string.return_value = "5"
        self.presenter.on_plus_clicked()
        self.mock_view.print_result.assert_called_once_with(8)

    def test_on_minus_clicked(self):
        self.mock_view.get_first_argument_as_string.return_value = "10"
        self.mock_view.get_second_argument_as_string.return_value = "4"
        self.presenter.on_minus_clicked()
        self.mock_view.print_result.assert_called_once_with(6)

    def test_on_multiply_clicked(self):
        self.mock_view.get_first_argument_as_string.return_value = "7"
        self.mock_view.get_second_argument_as_string.return_value = "3"
        self.presenter.on_multiply_clicked()
        self.mock_view.print_result.assert_called_once_with(21)

    def test_on_divide_clicked(self):
        self.mock_view.get_first_argument_as_string.return_value = "10"
        self.mock_view.get_second_argument_as_string.return_value = "2"
        self.presenter.on_divide_clicked()
        self.mock_view.print_result.assert_called_once_with(5)

    def test_on_divide_by_zero(self):
        self.mock_view.get_first_argument_as_string.return_value = "10"
        self.mock_view.get_second_argument_as_string.return_value = "0"
        self.presenter.on_divide_clicked()
        self.mock_view.display_error.assert_called_once_with("Division by zero is not allowed.")

    def test_invalid_input(self):
        self.mock_view.get_first_argument_as_string.return_value = "abc"
        self.mock_view.get_second_argument_as_string.return_value = "5"
        self.presenter.on_plus_clicked()
        self.mock_view.display_error.assert_called_once_with("Invalid input.")


# Запуск тестов
unittest.TextTestRunner().run(unittest.TestSuite([
    unittest.makeSuite(TestCalculator),
    unittest.makeSuite(TestCalculatorPresenter)
]))
