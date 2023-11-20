using Kursovay2.User;
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
    /// Логика взаимодействия для NewMemNow.xaml
    /// </summary>
    public partial class NewMemNow : Window
    {
        public NewMemNow()
        {
            InitializeComponent();
        }

        private void VernutObrat(object sender, RoutedEventArgs e)
        {
           Users users = new Users();
            users.Show();
            this.Close();

        }
    }
}
