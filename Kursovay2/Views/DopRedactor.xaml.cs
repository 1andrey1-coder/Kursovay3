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
    /// Логика взаимодействия для DopRedactor.xaml
    /// </summary>
    public partial class DopRedactor : Page
    {
        public DopRedactor() 
        {
            InitializeComponent();

        }

        private void ClickObratAddRof(object sender, RoutedEventArgs e)
        {
            AddRof.AddRof addRof = new AddRof.AddRof();
            //addRof.Show();
            //this.Close();
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {

        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {

        }
    }
}
