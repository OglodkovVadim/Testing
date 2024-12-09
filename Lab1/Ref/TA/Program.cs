using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TA1
{
    class Program
    {
        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(char с)
        {
            if ("+-/*()#".IndexOf(с) != -1)
                return true;
            return false;
        }

        //Метод возвращает true, если проверяемый символ - разделитель ("пробел")
        static private bool IsDelimeter(char c)
        {
            if (c == ' ' || c=='\t')
                return true;
            return false;
        }

        //Метод возвращает приоритет оператора
        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 1;
                case ')': return 1;
                case '+': return 2;
                case '-': return 2;
                case '*': return 3;
                case '/': return 3;
                default: return 4;
            }
        }

        //Метод, преобразующий входную строку с выражением в постфиксную запись
        static private string GetExpression(string input)
        {
            input = input.Replace(".", ",");
            while (input.IndexOf("pow") != -1)
                input = Regex.Replace(input, @"pow\((?<first>.*?),(?<second>.*?)\)", "((${first})#(${second}))");
            if (input.IndexOf(",,") != -1)
                return "Некоректный формат чисел!";

            string output = string.Empty; //Строка для хранения выражения
            Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов
            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {
                //Разделители пропускаем
                if (IsDelimeter(input[i]))
                    continue; //Переходим к следующему символу
                //Если символ - цифра, то считываем все число
                else if (char.IsDigit(input[i]) || input[i] == ',') //Если цифра 
                {
                    string tempOut = string.Empty;
                    //Читаем до разделителя или оператора, что бы получить число
                    while ((!IsDelimeter(input[i])) && !IsOperator(input[i]))
                    {
                        if (!char.IsDigit(input[i]) && input[i] != ',')
                        return "Ошибка! " + i + 1 + "-ый символ не является символом операции или цифрой";
                        tempOut += input[i]; //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу
                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }
                    output += tempOut + " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                }

                //Если символ - оператор
                else if (IsOperator(input[i])) //Если оператор
                {
                    if (i + 1 < input.Length)
                        if (input[i] == '(' && input[i + 1] == '-') //Если символ - открывающая скобка и "-" после нее
                            output += "0 "; //Записываем 0

                    if (input[i] == '(') //Если символ - открывающая скобка
                        operStack.Push(input[i]); //Записываем её в стек

                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        try
                        {
                            char s = operStack.Pop();

                            while (s != '(')
                            {
                                output += s.ToString() + ' ';
                                s = operStack.Pop();
                            }
                        }
                        catch (InvalidOperationException)
                        {
                            return "Ошибка! Неправильно расставлены скобки!";
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                        operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                    }
                }
                else return "Ошибка! " + (i + 1) + "-ый символ не является символом операции или цифрой";
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку 

            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }

        //Метод, вычисляющий значение выражения, уже преобразованного в постфиксную запись
        static private double Counting(string input)
        {
            double result = double.NaN; //Результат
            Stack<double> temp = new Stack<double>(); //Временный стек для решения 

            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (char.IsDigit(input[i])||input[i]==',')
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }
                    a = a.Replace(",", "0,");
                    temp.Push(Convert.ToDouble(a)); //Записываем в стек
                    i--;
                }
                else if (IsOperator(input[i])) //Если символ - оператор
                {
                    //Берем два последних значения из стека
                    try
                    {
                        double a = temp.Pop();
                        double b;
                        if (input[i] == '-' && temp.Count == 0)
                            b = 0;
                        else
                            b = temp.Pop();

                        switch (input[i]) //И производим над ними действие, согласно оператору
                        {
                            case '+':
                                result = b + a; break;
                            case '-':
                                result = b - a; break;
                            case '*':
                                result = b * a; break;
                            case '#':
                                if (b<0)
                                {
                                    Console.WriteLine("Нельзя возводить отрицательное число в степень!");
                                    return double.NaN;
                                }
                                result = Math.Pow(b,a); break;
                            case '/':
                                if (a == 0)
                                {
                                    Console.WriteLine("Деление на ноль!");
                                    return double.NaN;
                                }
                                result = b / a; break;
                        }
                        temp.Push(result); //Результат вычисления записываем обратно в стек
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Неверное количество служебных символов!");
                        return double.NaN;
                    }
                }
            }
            try
            {
                return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Отсутствуют арифметичесие операции");
                return double.NaN;
            }
        }

        //Метод Calculate принимает выражение в виде строки и возвращает результат, в своей работе использует другие методы класса
        static public double Calculate(string input)
        {
            string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
            Console.WriteLine("Преобразованное выражение: " + output.Replace("#", "pow"));
            double result = Counting(output); //Решаем полученное выражение
            return result; //Возвращаем результат
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите выражение: "); //Предлагаем ввести выражение
                string parsestr = Console.ReadLine();
                if (parsestr == "") break;
                
                Console.WriteLine("Результат: " + Calculate(parsestr)); //Считываем, и выводим результат
            }
        }
    }
}
