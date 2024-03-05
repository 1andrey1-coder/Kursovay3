using Kursovay2.API;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kursovay2.Views
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string login { get; set; }
        public string Password { get; set; }

      
        private async void SingIn(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = await Client.Instance.UserLogin(login, txtPassword.Password);
                if (user.RoleId == 2)
                {
                    Users users = new Users();
                    users.Show();

                }
                else if (user.RoleId == 1)
                {
                    Admin.Admin adminWindow = new Admin.Admin();
                    adminWindow.Show();

                }
                else
                {
                    MessageBox.Show("Логин/пароль неправильный", "Неудачный вход", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //Admin.Admin admin = new Admin.Admin();
                //admin.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }




        private void PerexodUser(object sender, RoutedEventArgs e)
        {

            User.Users user = new User.Users();
            //user.Show();
            //this.Close();


        }



        private void PerexodAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin();
            //admin.Show();
            //this.Close();

        }

        private void PerexodGost(object sender, RoutedEventArgs e)
        {

            Gost.Gost gost = new Gost.Gost();
            //gost.Show();
            //this.Close();

        }

        private void PerehodRegistr(object sender, RoutedEventArgs e)
        {

            Registr.Registr registr = new Registr.Registr();
            //registr.Show();
            //this.Close();

        }

        private void dragWindows(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void btnMinimize_Click(object sender, RoutedEventArgs e)
        //{

        //    //WindowState = WindowState.Minimized;
        //}
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void RegisterShow(object sender, RoutedEventArgs e)
        {
            Registr.Registr registr = new Registr.Registr();
            registr.Show();
            //this.Close();
        }


    }
}
