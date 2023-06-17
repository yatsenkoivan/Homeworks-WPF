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
        private static string data_path = "data.bin";
        public MainWindow()
        {
            InitializeComponent();
            ReadItems();
        }
        private void ReadItems()
        {
            FileStream fs = File.Open(data_path, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            while (fs.Position < fs.Length)
            {
                data.Items.Add(bf.Deserialize(fs));
            }
            fs.Close();
        }
        private void WriteItem(Task task)
        {
            FileStream fs = File.Open(data_path, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            foreach (object i in data.Items)
            {
                bf.Serialize(fs,i as Task);
            }
            fs.Close();
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
            DateTime date = new DateTime(yy, mm, dd, hours, minutes, 0);
            Task task = new Task(tb_name.Text, date);
            data.Items.Add(task);
            WriteItem(task);
        }
    }
}
