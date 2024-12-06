# calculator.py
class Calculator:
    def sum(self, a, b):
        return a + b

    def subtract(self, a, b):
        return a - b

    def multiply(self, a, b):
        return a * b

    def divide(self, a, b):
        if abs(b) < 1e-8:
            raise ArithmeticError("Division by zero is not allowed.")
        return a / b
