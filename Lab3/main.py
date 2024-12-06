import tkinter as tk
from tkinter import messagebox

class Calculator:
    """Interface for calculator operations."""

    def sum(self, a: float, b: float) -> float:
        return a + b

    def subtract(self, a: float, b: float) -> float:
        return a - b

    def multiply(self, a: float, b: float) -> float:
        return a * b

    def divide(self, a: float, b: float) -> float:
        if abs(b) < 1e-8:
            raise ArithmeticError("Division by zero is not allowed.")
        return a / b


class CalculatorView:
    """Interface for calculator view."""

    def print_result(self, result: float) -> None:
        print(f"Result: {result}")

    def display_error(self, message: str) -> None:
        print(f"Error: {message}")

    def get_first_argument_as_string(self) -> str:
        return input("Enter first number: ")

    def get_second_argument_as_string(self) -> str:
        return input("Enter second number: ")


class CalculatorPresenter:
    """Interface for calculator presenter."""

    def __init__(self, view: CalculatorView, calculator: Calculator):
        self.view = view
        self.calculator = calculator

    def on_plus_clicked(self) -> None:
        try:
            a = float(self.view.get_first_argument_as_string())
            b = float(self.view.get_second_argument_as_string())
            result = self.calculator.sum(a, b)
            self.view.print_result(result)
        except ValueError:
            self.view.display_error("Invalid input.")
        except Exception as e:
            self.view.display_error(str(e))

    def on_minus_clicked(self) -> None:
        try:
            a = float(self.view.get_first_argument_as_string())
            b = float(self.view.get_second_argument_as_string())
            result = self.calculator.subtract(a, b)
            self.view.print_result(result)
        except ValueError:
            self.view.display_error("Invalid input.")
        except Exception as e:
            self.view.display_error(str(e))

    def on_multiply_clicked(self) -> None:
        try:
            a = float(self.view.get_first_argument_as_string())
            b = float(self.view.get_second_argument_as_string())
            result = self.calculator.multiply(a, b)
            self.view.print_result(result)
        except ValueError:
            self.view.display_error("Invalid input.")
        except Exception as e:
            self.view.display_error(str(e))

    def on_divide_clicked(self) -> None:
        try:
            a = float(self.view.get_first_argument_as_string())
            b = float(self.view.get_second_argument_as_string())
            result = self.calculator.divide(a, b)
            self.view.print_result(result)
        except ValueError:
            self.view.display_error("Invalid input.")
        except ArithmeticError:
            self.view.display_error("Division by zero is not allowed.")
        except Exception as e:
            self.view.display_error(str(e))


class CalculatorUI(CalculatorView):
    """UI for Calculator using tkinter."""

    def __init__(self, root, presenter: CalculatorPresenter):
        self.presenter = presenter

        # Input fields
        self.first_arg_entry = tk.Entry(root, width=15)
        self.first_arg_entry.grid(row=0, column=0, padx=5, pady=5)

        self.second_arg_entry = tk.Entry(root, width=15)
        self.second_arg_entry.grid(row=0, column=2, padx=5, pady=5)

        # Buttons (commands temporarily set to None)
        self.add_button = tk.Button(root, text="+", width=5, command=None)
        self.add_button.grid(row=1, column=0, padx=5, pady=5)

        self.subtract_button = tk.Button(root, text="-", width=5, command=None)
        self.subtract_button.grid(row=1, column=1, padx=5, pady=5)

        self.multiply_button = tk.Button(root, text="*", width=5, command=None)
        self.multiply_button.grid(row=1, column=2, padx=5, pady=5)

        self.divide_button = tk.Button(root, text="/", width=5, command=None)
        self.divide_button.grid(row=1, column=3, padx=5, pady=5)

        # Result display
        self.result_label = tk.Label(root, text="Result: ", anchor="w")
        self.result_label.grid(row=2, column=0, columnspan=4, sticky="w", padx=5, pady=5)

    def set_presenter(self, presenter: CalculatorPresenter):
        self.presenter = presenter

        # Set button commands after presenter is assigned
        self.add_button.config(command=self.presenter.on_plus_clicked)
        self.subtract_button.config(command=self.presenter.on_minus_clicked)
        self.multiply_button.config(command=self.presenter.on_multiply_clicked)
        self.divide_button.config(command=self.presenter.on_divide_clicked)

    def print_result(self, result: float) -> None:
        self.result_label.config(text=f"Result: {result}")

    def display_error(self, message: str) -> None:
        messagebox.showerror("Error", message)

    def get_first_argument_as_string(self) -> str:
        return self.first_arg_entry.get()

    def get_second_argument_as_string(self) -> str:
        return self.second_arg_entry.get()


# Setting up the application
root = tk.Tk()
root.title("Calculator")

# Create calculator and UI
calculator = Calculator()
ui = CalculatorUI(root, None)  # Temporarily pass None for presenter

# Create presenter and set it to UI
presenter = CalculatorPresenter(ui, calculator)
ui.set_presenter(presenter)

# Start main loop
root.mainloop()


