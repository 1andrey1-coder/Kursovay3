using Kursovay2.API;
using Kursovay2.Models;
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
    /// Логика взаимодействия для Admini_We_.xaml
    /// </summary>
    public partial class Admini_We_ : Window
    {
        private readonly LoginUserDTO user;

        public Admini_We_()
        {
            InitializeComponent();

        }

        private void VernutObrat(object sender, RoutedEventArgs e)
        {
            Users users = new Users();
            //Users users = new Users(user);
            users.Show();
            this.Close();
        }
    }
}
