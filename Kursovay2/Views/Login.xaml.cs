using Kursovay2.API;
using Kursovay2.mvvm;
using Kursovay2.mvvm.VM;
using Kursovay2.User;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly LoginUserDTO user;

        public Login()
        {
            InitializeComponent();
            var vm = (LoginVM)DataContext;
            vm.RegisterPassBox(passwordBox);
        }

       



        private void PerexodUser(object sender, RoutedEventArgs e)
        {

            User.Users user2 = new User.Users(user);
            //user.Show();
            //this.Close();


        }



        private void PerexodAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(user);
            //admin.Show();
            //this.Close();

        }

        private void PerexodGost(object sender, RoutedEventArgs e)
        {

            Gost.Gost gost = new Gost.Gost();
            gost.Show();
            Window.GetWindow(this).Close();

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

        //    WindowState = WindowState.Minimized;
        //}
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.WindowState = WindowState.Minimized;
        }

        private void RegisterShow(object sender, RoutedEventArgs e)
        {
            Registr.Registr registr = new Registr.Registr();
            registr.Show();
            Application.Current.MainWindow.Close();
        }

        private void ResetParol(object sender, RoutedEventArgs e)
        {
            ResetPassword resetPassword = new ResetPassword();
            resetPassword.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
