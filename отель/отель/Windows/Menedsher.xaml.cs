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
using System.Windows.Shapes;

namespace отель.Windows
{
    /// <summary>
    /// Логика взаимодействия для Menedsher.xaml
    /// </summary>
    public partial class Menedsher : Window
    {
        public Menedsher()
        {
            InitializeComponent();
        }

        private void Clienta(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            client.Show();
            Close();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow menedsher = new MainWindow();
            menedsher.Show();
            Close();
        }

        private void Gerister(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            Close();
        }
    }
}
