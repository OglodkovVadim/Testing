# features/steps/calculator_steps.py
from behave import given, when, then
from my_calc import Calculator
from nose.tools import assert_equal, assert_raises

@given('I have entered {number:d} into the calculator')
def step_given_number(context, number):
    if not hasattr(context, 'numbers'):
        context.numbers = []
    context.numbers.append(number)

@when('I press {operation}')
def step_when_operation(context, operation):
    calculator = Calculator()
    a, b = context.numbers
    try:
        if operation == "add":
            context.result = calculator.sum(a, b)
        elif operation == "subtract":
            context.result = calculator.subtract(a, b)
        elif operation == "multiply":
            context.result = calculator.multiply(a, b)
        elif operation == "divide":
            context.result = calculator.divide(a, b)
    except Exception as e:
        context.error = str(e)

@then('the result should be {expected_result:d}')
def step_then_result(context, expected_result):
    assert_equal(context.result, expected_result)

@then('an error should be raised with message "{message}"')
def step_then_error(context, message):
    assert_equal(context.error, message)
