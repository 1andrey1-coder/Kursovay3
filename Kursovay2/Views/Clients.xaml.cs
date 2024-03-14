using Kursovay2.API;
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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        private readonly LoginUserDTO user;

        public Clients()
        {
            InitializeComponent();

        }

        private void ClickObrat(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(user);
            admin.Show();
            this.Close();
        }
    }
}
