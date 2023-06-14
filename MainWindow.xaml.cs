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

namespace Homeworks_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static double opacity = 0.51;
        static double default_opacity = 1;
        public MainWindow()
        {
            InitializeComponent();
            TextBox_LostFocus(null, null);
            PasswordBox_LostFocus(null, null);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            EmailBorder.Opacity = opacity;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailBorder.Opacity = default_opacity;
        }
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBorder.Opacity = opacity;
        }
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBorder.Opacity = default_opacity;
        }

        private void EmailBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailInput.Focus();
        }

        private void PasswordText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordInput.Focus();
        }
    }
}
