using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    class Validation
    {
        private string InputText { set; get; }
        private string OutputText { set; get; }

        public Validation(string tIn, string tOut)
        {
            this.InputText = tIn;
            this.OutputText = tOut;
        }

        //sprawdza czy przecinki sa na poprawnych pozycjach
        private bool CheckIfCommasAreGood()
        {
            var text = InputText;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',') //wyszukano przecinek
                {
                    if (i == 0) //jesli jest to pierwszy indeks to blad
                        return false;
                    if (text[i + 1] == ',') //jesli zaraz za wykrytym przecinkiem jest kolejny to blad
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //sprawdza czy wystepuja powtorzenia operatorow
        private bool CheckIfOperatorsAreGood()
        {
            var text = InputText;
            var howManyOperatorsDetected = 0;

            foreach (char c in text)
            {
                if (!Char.IsNumber(c) && c != ',') //zliczanie znakow niebedacych cyframi i przecinkiem
                    howManyOperatorsDetected++;
            }

            if (howManyOperatorsDetected >= 100) //zmienic
                return false;
            else return true;
        }

        //sprawdza strumien z operacja do wykonania
        public void CheckIfCorrectInput()
        {
            if ((InputText.LastIndexOf('+') == InputText.Length - 1)
                || (InputText.LastIndexOf('-') == InputText.Length - 1)
                || (InputText.LastIndexOf('*') == InputText.Length - 1)
                || (InputText.LastIndexOf('/') == InputText.Length - 1)
                || (InputText.LastIndexOf(',') == InputText.Length - 1)
                || InputText.IndexOf('+') == 0
                || InputText.IndexOf('-') == 0
                || InputText.IndexOf('*') == 0
                || InputText.IndexOf('/') == 0
                || InputText.IndexOf(',') == 0)
            {//sprawdzenie czy na samym poczatku lub koncu jest jakis operator
                MessageBox.Show("Incorrect input!");
            }
            else if (InputText.Length == 0)
            {//sprawdzenie czy niewprowadzono pustego stringa
                MessageBox.Show("No input!");
            }
            else if ((!CheckIfCommasAreGood()) || (!CheckIfOperatorsAreGood())) //sprawdzenie separatorow i operatorow
                MessageBox.Show("Badly placed commas/operators!");
            else
            {
                Calculations calc = new Calculations(InputText);
                OutputText = Convert.ToString(calc.CalculationOfOperation());
            }
        }
    }
}
