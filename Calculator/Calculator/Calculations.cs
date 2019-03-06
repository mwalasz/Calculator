using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Calculations
    {
        private string Text { set; get; } //tekst wejsciowy do analizy

        public Calculations(string text)
        {
            this.Text = text;
        }

        private double ValueOfComponentOperation(char operatorOfOperation, double a, double b)
        {
            switch (operatorOfOperation)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                default:
                    return 0;
            }
        }

        private double ValueOfEntireOperation(ref List<double> lNumbers, ref List<char> lOperators)
        {
            double output = 0d, tempD = 0d;
            int count = lNumbers.Count;

            for (int i = 0; i < count; i++)
            {
                if (i < count - 1) //zabezpieczenie przed wyjsciem poza zakres ilosci operatorow
                {
                    if (count % 3 == 0 && i == 0)
                    {
                        //operatory o nizszej wadze jako pierwsze w dzialaniu
                        if (lOperators[i] == '+' || lOperators[i] == '-')
                        {
                            if (lOperators[i + 1] == '*' || lOperators[i + 1] == '/')
                            {
                                tempD = ValueOfComponentOperation(lOperators[i + 1], lNumbers[i + 1], lNumbers[i + 2]);
                                output = ValueOfComponentOperation(lOperators[i], lNumbers[i], tempD);

                            }
                        } //operatory o wyzszej wadze jako pierwsze w dzialaniu
                        else if (lOperators[i] == '*' || lOperators[i] == '/')
                        {
                            output += ValueOfComponentOperation(lOperators[i], lNumbers[i], lNumbers[i + 1]);
                            output = ValueOfComponentOperation(lOperators[i + 1], output, lNumbers[i + 2]);
                        }
                    }
                    else if (count % 3 == 2 && i == 0)
                    {
                        output = ValueOfComponentOperation(lOperators[i], lNumbers[i], lNumbers[i + 1]);
                    }
                }
            }

            return output;
        }
        //wyszukiwanie wszystkich operatorow w stringu wejsciowym i dodanie ich do listy
        private void FindOperators(ref List<char> list, string input)
        {
            foreach (char c in input)
            {
                if (!char.IsNumber(c) && c != ',')
                {
                    list.Add(c);
                }
            }
        }

        //wyszukiwanie wszystkich liczb w stringu wejsciowym i dodanie ich do listy
        private void FindNumbers(ref List<double> list, string input)
        {
            string tempS = "";
            int tLength = input.Length;

            for (int i = 0; i < tLength; i++)
            {
                if (input[i] != '+' && input[i] != '-' && input[i] != '*' && input[i] != '/')
                    tempS += input[i];
                else
                {
                    list.Add(Convert.ToDouble(tempS));
                    tempS = string.Empty;
                }

                if (i == tLength - 1) //ostatnia cyfra
                {
                    list.Add(Convert.ToDouble(tempS));
                    tempS = string.Empty;
                }
            }
        }

        public double CalculationOfOperation()
        {
            List<char> operatorsFromInput = new List<char>(); //lista z wszystkimi operatorami
            List<double> numbersFromInput = new List<double>(); //lista z wszystkimi liczbami

            FindOperators(ref operatorsFromInput, Text);

            FindNumbers(ref numbersFromInput, Text);

            if (operatorsFromInput.Count == 0)
                return numbersFromInput[0];
            else return ValueOfEntireOperation(ref numbersFromInput, ref operatorsFromInput);
        }
    }
}
