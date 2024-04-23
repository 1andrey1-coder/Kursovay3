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
    /// Логика взаимодействия для Library.xaml
    /// </summary>
    public partial class Library : Window
    {
        public Library()
        {
            InitializeComponent();
        }

        private void ClickToMainWindow(object sender, RoutedEventArgs e)
        {
            if (SingleProfle.user.RoleId == 1)
            {

                Admin.Admin adminWindow = new Admin.Admin(SingleProfle.user);
                adminWindow.Show();
                Close();
            }
            if (SingleProfle.user.RoleId == 2)
            {
                User.Users userWindow = new User.Users(SingleProfle.user);
                userWindow.Show();
                Close();
            }
        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {

        }

        private void Focus(object sender, RoutedEventArgs e)
        {

        }

        private void lastFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
