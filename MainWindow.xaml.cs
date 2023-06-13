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

namespace main_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int maxLen = 16;
        static int inputFont = 72;
        static double button_font_ratio = 3;
        char? operation;
        public MainWindow()
        {
            InitializeComponent();
            Label_Operations.Content = "";
            Label_Input.Content = "0";
        }
        private void InputDelete()
        {
            int len = Label_Input.Content.ToString().Length;
            if (len > 0)
            {
                Label_Input.Content = Label_Input.Content.ToString().Remove(len - 1);
                if (Label_Input.Content.ToString() == "") Label_Input.Content = "0";
                double temp;
                if (double.TryParse(Label_Input.Content.ToString(), out temp) == false)
                {
                    Label_Input.Content = "0";
                }
            }
            FontFix();
        }
        private void FontFix()
        {
            double ratio = SystemParameters.PrimaryScreenWidth / SystemParameters.PrimaryScreenHeight;
            Label_Input.FontSize = Math.Min(inputFont, Label_Input.ActualWidth / Label_Input.Content.ToString().Length * ratio);
        }
        private bool CheckLength()
        {
            return Label_Input.Content.ToString().Length < maxLen;
        }
        private void Input(Key key)
        {
            if (key == Key.Back)
            {
                InputDelete();
            }
            if (key == Key.Enter)
            {
                button_equal_Click(null, null);
            }
            FontFix();
        }
        private void InputDigit(char digit)
        {
            if (CheckLength() && digit >= '0' && digit <= '9')
            {
                if (Label_Input.Content.ToString() == "0") Label_Input.Content = "";
                Label_Input.Content = Label_Input.Content.ToString() + digit;
                FontFix();
            }
        }
        private void Window1_KeyDown(object sender, KeyEventArgs e)
        {
            Input(e.Key);
        }

        private void button_del_Click(object sender, RoutedEventArgs e)
        {
            InputDelete();
        }

        private void button_digit_Click(object sender, RoutedEventArgs e)
        {
            InputDigit((sender as Button).Content.ToString()[0]);
        }
        private void ButtonsFontFix()
        {
            Button button;
            foreach (object b in MainGrid.Children)
            {
                if (b is Button)
                {
                    button = b as Button;
                    button.FontSize = Math.Min(button.ActualHeight, button.ActualWidth) / button_font_ratio;
                } 
            }
        }
        private void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window1.Width >= 500)
            {
                Scroll.Visibility = Visibility.Visible;
                clearGrid.Visibility = Visibility.Visible;
            }
            else
            {
                Scroll.Visibility = Visibility.Collapsed;
                clearGrid.Visibility = Visibility.Collapsed;
            }
            FontFix();
            ButtonsFontFix();
        }
        private void OperationLabel_Show()
        {
            Label_Operations.Content = Label_Input.Content.ToString() + " " + operation;
            Label_Input.Content = "0";
        }
        private void Calculate()
        {
            if (Label_Operations.Content.ToString() == "" || operation == '=')
            {
                Label_Operations.Content = Label_Input.Content;
                return;
            }
            double val = double.Parse(Label_Operations.Content.ToString().Split(' ')[0]);
            double newval = double.Parse(Label_Input.Content.ToString());
            Label_Operations.Content = val + " " + operation + " " + newval;
            double result = val;
            switch (operation)
            {
                case '+':
                    result += newval;
                    break;
                case '-':
                    result -= newval;
                    break;
                case '*':
                    result *= newval;
                    break;
                case '/':
                    if (newval == 0)
                    {
                        MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    result /= newval;
                    break;
            }

            TextBox tb_operation = new TextBox();
            tb_operation.Margin = new Thickness(5, 5, 5, 5);
            tb_operation.IsReadOnly = true;
            tb_operation.Foreground = Brushes.Gray;
            tb_operation.BorderThickness = new Thickness(0);
            tb_operation.Text = Label_Operations.Content.ToString() + '=';
            tb_operation.FontSize = 12;
            tb_operation.HorizontalAlignment = HorizontalAlignment.Right;

            TextBox tb_result = new TextBox();
            tb_result.Margin = new Thickness(5, 5, 5, 5);
            tb_result.IsReadOnly = true;
            tb_result.BorderThickness = new Thickness(0);
            tb_result.Text = result.ToString();
            tb_result.FontSize = 18;
            tb_result.HorizontalAlignment = HorizontalAlignment.Right;

            if (SidePanel.Children.Count == 0)
            {
                clear_button.Visibility = Visibility.Visible;             

            }
            SidePanel.Children.Insert(0, tb_result);
            SidePanel.Children.Insert(0, tb_operation);
            Label_Input.Content = result;
            Label_Input.Content = Label_Input.Content.ToString().Replace('.', ',');
            if (Label_Input.Content.ToString().Length > maxLen)
            {
                Label_Input.Content = Label_Input.Content.ToString().Remove(maxLen);
            }
            FontFix();
            operation = '=';
        }

        private void button_operation_Click(object sender, RoutedEventArgs e)
        {
            if (operation != null)
            {
                Calculate();
            }
            operation = (sender as Button).Content.ToString()[0];
            OperationLabel_Show();
        }

        private void button_dot_Click(object sender, RoutedEventArgs e)
        {
            Input(Key.OemComma);
        }

        private void button_equal_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
            Label_Operations.Content = Label_Operations.Content.ToString() + " =";
        }

        private void button_C_Click(object sender, RoutedEventArgs e)
        {
            Label_Input.Content = "0";
            FontFix();
        }

        private void button_CE_Click(object sender, RoutedEventArgs e)
        {
            button_C_Click(sender, e);
            operation = null;
            Label_Operations.Content = "";
            FontFix();
        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            SidePanel.Children.Clear();
            clear_button.Visibility = Visibility.Collapsed;
        }
    }
}
