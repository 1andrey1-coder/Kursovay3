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

namespace Kursovay2.AddRof
{
    /// <summary>
    /// Логика взаимодействия для AddRof.xaml
    /// </summary>
    public partial class AddRof : Window
    {
        public AddRof()
        {
            InitializeComponent();
        }

        private void ClickObratAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin();
            admin.Show();
            this.Close();
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {

        }

        private void ClickDopRedactor(object sender, RoutedEventArgs e)
        {
            Views.DopRedactor dopRedactor = new Views.DopRedactor();
            dopRedactor.Show();
            this.Close();

        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {

        }

        private void AddRof2(object sender, RoutedEventArgs e)
        {

        }

        private void Opisania(object sender, RoutedEventArgs e)
        {

        }
    }
}
