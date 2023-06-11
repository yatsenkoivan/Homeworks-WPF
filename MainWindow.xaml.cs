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
        List<Button> buttons;
        char? operation;
        public MainWindow()
        {
            InitializeComponent();
            Label_Operations.Content = "";
            Label_Input.Content = "0";
            buttons = new List<Button>();
            buttons.Add(button_0);
            buttons.Add(button_1);
            buttons.Add(button_2);
            buttons.Add(button_3);
            buttons.Add(button_4);
            buttons.Add(button_5);
            buttons.Add(button_6);
            buttons.Add(button_7);
            buttons.Add(button_8);
            buttons.Add(button_9);
            buttons.Add(button_add);
            buttons.Add(button_subtract);
            buttons.Add(button_mul);
            buttons.Add(button_divide);
            buttons.Add(button_del);
            buttons.Add(button_C);
            buttons.Add(button_CE);
            buttons.Add(button_dot);
            buttons.Add(button_equal);
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
            if (Label_Input.Content.ToString().Length == maxLen) return;
            if ((int)key >= 34 && (int)key <= 43)
            {
                if (Label_Input.Content.ToString() == "0") Label_Input.Content = "";
                Label_Input.Content = Label_Input.Content.ToString() + key.ToString()[1];
            }
            if (key == Key.OemComma && Label_Input.Content.ToString().Contains(',') == false)
            {
                Label_Input.Content = Label_Input.Content.ToString() + ",";
            }
            FontFix();
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
            Key key;
            char digit = (sender as Button).Content.ToString()[0];
            switch (digit)
            {
                case '0':
                    key = Key.D0;
                    break;
                case '1':
                    key = Key.D1;
                    break;
                case '2':
                    key = Key.D2;
                    break;
                case '3':
                    key = Key.D3;
                    break;
                case '4':
                    key = Key.D4;
                    break;
                case '5':
                    key = Key.D5;
                    break;
                case '6':
                    key = Key.D6;
                    break;
                case '7':
                    key = Key.D7;
                    break;
                case '8':
                    key = Key.D8;
                    break;
                case '9':
                    key = Key.D9;
                    break;
                default:
                    return;
            }
            Input(key);
        }
        private void ButtonsFontFix()
        {
            foreach (Button b in buttons)
            {
                b.FontSize = Math.Min(b.ActualHeight, b.ActualWidth) / button_font_ratio;
            }
        }
        private void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window1.Width >= 500)
            {
                Scroll.Visibility = Visibility.Visible;
            }
            else
            {
                Scroll.Visibility = Visibility.Collapsed;
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
                Button clear_button = new Button();
                clear_button.Width = 40;
                clear_button.Height = 40;
                clear_button.Content = "🗑";

                clear_button.Click += delegate
                {
                    SidePanel.Children.Clear();
                    clearGrid.Children.Clear();
                };

                clearGrid.Children.Add(clear_button);

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
    }
}
