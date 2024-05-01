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
    /// Логика взаимодействия для ProfileAdmin.xaml
    /// </summary>
    public partial class ProfileAdmin : Window
    {
        public ProfileAdmin()
        {
            InitializeComponent();
            DisplayUserInfo();
            LoadDefaultImage();
        }



        private async void DisplayUserInfo()
        {

            LoginUserDTO login1 = await Client.Instance.GetUser(SingleProfle.user.LoginId);

            if (login1 != null)
            {

                textBlockUserName.Text = login1.LoginName;
                textBlockMail.Text = login1.Mail;

            }
            else
            {
                textBlockUserName.Text = "User not found";
                textBlockMail.Text = "User not found";
            }





        }
        byte[] defaultImage;
        private void LoadDefaultImage()
        {
            var stream = Application.GetResourceStream(new Uri("Images\\ImageNull2png", UriKind.Relative));
            defaultImage = new byte[stream.Stream.Length];
            stream.Stream.Read(defaultImage, 0, defaultImage.Length);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            Close();
            //Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.WindowState = WindowState.Minimized;
        }
        
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            //Window parentWindow = Window.GetWindow(this);
            //parentWindow.WindowState = WindowState.Maximized;
            if (WindowState == WindowState.Normal)
            {
                // Меняем размер окна на максимальный
                WindowState = WindowState.Maximized;
            }
            else
            {
                // Меняем размер окна на обычный
                WindowState = WindowState.Normal;
            }
        }
        private void Back(object sender, RoutedEventArgs e)
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

        private async void ResetPassword(object sender, RoutedEventArgs e)
        {
            int id = SingleProfle.user.LoginId;
            string newPassword = resetPassword.Text;
            string newLogin = resetLogin.Text;
            string newMail = resetMail.Text;
            await Client.Instance.Profile(id, newPassword, newLogin, newMail);
        }

        private void AddPhoto(object sender, RoutedEventArgs e)
        {

        }
    }
}
