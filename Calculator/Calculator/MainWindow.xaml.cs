using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int NUMBER_OF_COMPONENTS = 3;
        private bool numberEntered = false;

        public MainWindow()
        {
            InitializeComponent();
            NumericCheckbox.IsChecked = true; //nie dziala w xaml?
        }

        //sprawdza strumien z operacja do wykonania
        private void CheckIfCorrectInput()
        {
            if ((InputTextBox.Text.LastIndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf('/') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.LastIndexOf(',') == InputTextBox.Text.Length - 1)
               || InputTextBox.Text.IndexOf('+') == 0 
               || InputTextBox.Text.IndexOf('-') == 0 
               || InputTextBox.Text.IndexOf('*') == 0
               || InputTextBox.Text.IndexOf('/') == 0
               || InputTextBox.Text.IndexOf(',') == 0)
            {//sprawdzenie czy na samym poczatku lub koncu jest jakis operator
                MessageBox.Show("Incorrect input!");
            }
            else if(InputTextBox.Text.Length == 0)
            {//sprawdzenie czy niewprowadzono pustego stringa
                MessageBox.Show("No input!");
            }
            else if((!CheckIfCommasAreGood()) || (!CheckIfOperatorsAreGood())) //sprawdzenie separatorow i operatorow
                MessageBox.Show("Badly placed commas/operators!");
            else
            {
                Calculations calc = new Calculations(InputTextBox.Text);
                this.OutputTextBox.Text = Convert.ToString(calc.CalculationOfOperation());
            }
        }

        //sprawdza czy przecinki sa na poprawnych pozycjach
        private bool CheckIfCommasAreGood()
        {
            var text = InputTextBox.Text;

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
            var text = InputTextBox.Text;
            var howManyOperatorsDetected = 0;

            foreach (char c in text)
            {
                if (!Char.IsNumber(c) && c != ',') //zliczanie znakow niebedacych cyframi i przecinkiem
                    howManyOperatorsDetected++;
            }

            if (howManyOperatorsDetected >= NUMBER_OF_COMPONENTS)
                return false;
            else return true;
        }

        //ustawia kursor na wejsciowe pole tekstowe
        private void SetFocusToMainInputBox()
        {
            InputTextBox.Focus();
            InputTextBox.CaretIndex = InputTextBox.Text.Length;
        }

        //czysci zawartosc obydwoch pol tekstowych
        private void ClearTextBoxes()
        {
            this.InputTextBox.Text = string.Empty;
            this.InputTextBox.Text = OutputTextBox.Text;
            OutputTextBox.Text = string.Empty;
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9) //sprawdzenie klawiszy na gorze klawiatury
            {
                numberEntered = true;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) //sprawdzenie klawiszy na numpadzie
            {
                numberEntered = true;
            }
            else if (e.Key == Key.OemComma || e.Key == Key.Decimal || e.Key == Key.Subtract || e.Key == Key.Add || e.Key == Key.Divide || e.Key == Key.Multiply || e.Key == Key.Enter)
                numberEntered = true;
            else //wykryto nieprawidlowe znaki
            {
                e.Handled = true; //zapobieganie wyswietleniu takiego znaku
                MessageBox.Show("Wrong character detected!");
            }

            if (e.Key == Key.Return)
            {
                CheckIfCorrectInput();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Empty input!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) //obydwa pelne
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "+";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
            {//sprawdzenie czy znak sie nie podwaja
                MessageBox.Show("Doubled symbols!");
            }
            else
            {
                this.InputTextBox.Text += "+";
            }
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void SumButton_Click(object sender, RoutedEventArgs e)
        {
            CheckIfCorrectInput(); //sprawdzenie poprawnosci danych wejsciowych
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Empty input!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) //obydwa pelne
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "-";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
                {//sprawdzenie czy znak sie nie podwaja
                MessageBox.Show("Doubled symbols!");
            }
            else
            {
                this.InputTextBox.Text += "-";
            }
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Empty input!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) //obydwa pelne
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "*";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
            {//sprawdzenie czy znak sie nie podwaja
                MessageBox.Show("Doubled symbols!");
            }
            else
            {
                this.InputTextBox.Text += "*";
            }
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            if(InputTextBox.Text.Length == 0 && OutputTextBox.Text.Length == 0)
            {
                MessageBox.Show("Empty input!");
            }
            else if (InputTextBox.Text.Length != 0 && OutputTextBox.Text.Length != 0) //obydwa pelne
            {
                ClearTextBoxes();
                this.InputTextBox.Text += "/";
            }
            else if ((InputTextBox.Text.IndexOf('+') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('-') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('*') == InputTextBox.Text.Length - 1)
               || (InputTextBox.Text.IndexOf('/') == InputTextBox.Text.Length - 1))
            {//sprawdzenie czy znak sie nie podwaja
                MessageBox.Show("Doubled symbols!");
            }
            else
            {
                this.InputTextBox.Text += "/";
            }
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void NumericCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            NumberKeySection.Visibility = Visibility.Visible; //zmiana widzialnosci
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void NumericCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            NumberKeySection.Visibility = Visibility.Collapsed; //zmiana widzialnosci
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //wyczyszczenie pol tekstowych
            InputTextBox.Text = string.Empty;
            OutputTextBox.Text = string.Empty;
            //ustawienie aktywnosci kursora na polu wejsciowym
            InputTextBox.Focus();
            InputTextBox.CaretIndex = InputTextBox.Text.Length;
        }

        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "9";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "8";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "7";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "6";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "5";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "4";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "3";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "2";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "1";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void NullButton_Click(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += "0";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }

        private void CommaButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InputTextBox.Text += ",";
            SetFocusToMainInputBox(); //ustawienie aktywnosci kursora na polu wejsciowym
        }
    }
}
