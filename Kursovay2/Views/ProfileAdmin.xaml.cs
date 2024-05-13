using Kursovay2.API;
using Kursovay2.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            SelectedProfile = new LoginUserDTO();

            LoadDefaultImage();
            DisplayUserInfo();
        }



        private async void DisplayUserInfo()
        {

            LoginUserDTO login = await Client.Instance.GetUser(SingleProfle.User.LoginId);
            
            if (login != null)
            {
                textBlockUserName.Text = login.LoginName;
                textBlockMail.Text = login.Mail;
                if (login.LoginImage != null)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(login.LoginImage);
                    bitmapImage.EndInit();
                    imageText.Source = bitmapImage;
                }
                else
                {
                    Uri uri = new Uri(Environment.CurrentDirectory + "\\Images\\ImageNull2.png", UriKind.Absolute);
                    BitmapImage img = new BitmapImage(uri);
                    imageText.Source = img;
                }
            }
            else
            {
                textBlockUserName.Text = "User not found";
                textBlockMail.Text = "User not found";
            }





        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        byte[] defaultImage;
        private LoginUserDTO selectedProfile;

        private void LoadDefaultImage()
        {
            //var stream = Application.GetResourceStream(new Uri("\\Images\\ImageNull2.png", UriKind.Relative));
            //defaultImage = new byte[stream.Stream.Length];
            //stream.Stream.Read(defaultImage, 0, defaultImage.Length);
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
            if (SingleProfle.User.RoleId == 1)
            {

                Admin.Admin adminWindow = new Admin.Admin(SingleProfle.User);
                adminWindow.Show();
                Close();
            }
            if (SingleProfle.User.RoleId == 2)
            {
                User.Users userWindow = new User.Users(SingleProfle.User);
                userWindow.Show();
                Close();
            }

        }

        private async void ResetPassword(object sender, RoutedEventArgs e)
        {

            int id = SingleProfle.User.LoginId;
            string newPassword = resetPassword.Text;
            string newLogin = resetLogin.Text;
            string newMail = resetMail.Text;
            //byte[] image = SelectedProfile.LoginImage;

            if (newPassword != null && newLogin != null && newMail != null)
            {
                SelectedProfile = new LoginUserDTO
                {
                    LoginId = id,
                    LoginName = newLogin,
                    LoginPassword = newPassword,
                    Mail = newMail,
                    LoginImage = SelectedProfile.LoginImage
                };
                //int selectedStatusId =  statusId.StatusId;

            }
            //await Client.Instance.Profile(id, newPassword, newLogin, newMail);
            await Client.Instance.Profile(SelectedProfile);
        }

        public LoginUserDTO SelectedProfile
        {
            get => selectedProfile;
            set
            {
                selectedProfile = value;
                Signal();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        private void AddPhoto(object sender, RoutedEventArgs e)
        {
            string dir = Environment.CurrentDirectory + @"\Images\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images|*.jpg;";
            if (dlg.ShowDialog() == true)
            {
                var test = new BitmapImage(new Uri(dlg.FileName));
                if (test.PixelWidth > 2000 || test.PixelHeight > 2000)
                {
                    MessageBox.Show("Картинка слишком большая");
                    return;
                }
                string newFile = dir + new FileInfo(dlg.FileName).Name;
                File.Copy(dlg.FileName, newFile, true);
                SelectedProfile.LoginImage = File.ReadAllBytes(newFile);
                Signal("SelectedProfile");
            }


        }

        private void podskaska(object sender, RoutedEventArgs e)
        {
            ProfileAdminPodskaska profileAdminPodskaska = new ProfileAdminPodskaska();
            profileAdminPodskaska.Show();

        }
    }
}
