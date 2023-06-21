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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Homeworks_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            data_SelectionChanged(null, null);
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text.Length == 0)
            {
                MessageBox.Show("Name cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int yy;
            int mm;
            int dd;
            int hours;
            int minutes;
            uint priority;
            try
            {
                dd = int.Parse(tb_days.Text);
                mm = int.Parse(tb_months.Text);
                yy = int.Parse(tb_years.Text);
                DateTime temp = new DateTime(yy, mm, dd);
            }
            catch (Exception)
            {
                MessageBox.Show("Date value is invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                hours = int.Parse(tb_hours.Text);
                minutes = int.Parse(tb_minutes.Text);
                DateTime temp = new DateTime(1, 1, 1,hours,minutes,0);
            }
            catch (Exception)
            {
                MessageBox.Show("Time value is invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (uint.TryParse(tb_priority.Text, out priority) == false)
            {
                MessageBox.Show("Priority value is invalid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime date = new DateTime(yy, mm, dd, hours, minutes, 0);
            Task task = new Task(tb_name.Text, date, priority);
            foreach (object i in data.Items)
            {
                if ((i as Task).Name == task.Name)
                {
                    MessageBox.Show("Task with such name already exist", "Could not add task", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            data.Items.Add(task);
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            if (data.SelectedIndex == -1 || data.SelectedIndex >= data.Items.Count) return;
            int index = data.SelectedIndex;
            data.Items.RemoveAt(index);
        }

        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.SelectedIndex == -1) button_remove.IsEnabled = false;
            else button_remove.IsEnabled = true;
        }
    }
}
