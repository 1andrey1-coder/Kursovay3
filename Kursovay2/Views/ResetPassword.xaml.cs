using Kursovay2.API;
using Kursovay2.mvvm.VM;
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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Kursovay2.Views
{
    /// <summary>
    /// Логика взаимодействия для ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        public string Mail { get; set; }

        public ResetPassword()
        {
            InitializeComponent();
           
        }
       


        private async void Reset(object sender, RoutedEventArgs e)
        {
            try
            {
                Client.Instance.PostSmsEmail(txtMail.Text);

                CodeСonfirmation codeСonfirmation = new CodeСonfirmation(txtMail.Text);
                codeСonfirmation.ShowDialog();

                if (codeСonfirmation.Success)
                {
                    var user = await Client.Instance.UserReset(txtMail.Text);
                    MessageBox.Show("Вы сбросили пароль для вашей почты", "Новый пароль отправлен вам на почту",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
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
    }
}
