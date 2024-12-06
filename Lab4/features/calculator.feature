# features/calculator.feature
Feature: Calculator operations
  As a user
  I want to perform basic arithmetic operations
  So that I can use the calculator effectively

  Scenario: Adding two numbers
    Given I have entered 5 into the calculator
    And I have entered 3 into the calculator
    When I press add
    Then the result should be 8

  Scenario: Subtracting two numbers
    Given I have entered 10 into the calculator
    And I have entered 4 into the calculator
    When I press subtract
    Then the result should be 6

  Scenario: Multiplying two numbers
    Given I have entered 7 into the calculator
    And I have entered 3 into the calculator
    When I press multiply
    Then the result should be 21

  Scenario: Dividing two numbers
    Given I have entered 10 into the calculator
    And I have entered 2 into the calculator
    When I press divide
    Then the result should be 5

  Scenario: Division by zero
    Given I have entered 10 into the calculator
    And I have entered 0 into the calculator
    When I press divide
    Then an error should be raised with message "Division by zero is not allowed."
