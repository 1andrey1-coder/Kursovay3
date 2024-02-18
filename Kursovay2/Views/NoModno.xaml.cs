using Kursovay2.Models;
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

namespace Kursovay2.Views
{
    /// <summary>
    /// Логика взаимодействия для NoModno.xaml
    /// </summary>
    public partial class NoModno : Page
    {
        public NoModno()
        {
            InitializeComponent();

        }

        private void VernutObrat(object sender, RoutedEventArgs e)
        {
           Gost.Gost gost = new Gost.Gost();
            //gost.Show();
            //this.Close();
        }
    }
}
