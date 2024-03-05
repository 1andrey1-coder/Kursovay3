using Kursovay2.API;
using Kursovay2.Models;
using Kursovay2.Views;
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

namespace Kursovay2.Registr
{
    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Window
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Registr() 
        {
            InitializeComponent();

        }

        private async void SingUp(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (PasswordBox != ConfirmPasswordBox)
                //{
                //    MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}

                var user = await Client.Instance.UserRegister(txtUser.Text, txtPassword.Password);
                MainWindow mainWindow = new MainWindow();
                MessageBox.Show("Вы зарегистрировались", "Успешная регистрация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }




        private void PerehodMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            this.Close();

        }

        private void dragWindows(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
