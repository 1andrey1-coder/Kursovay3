using Kursovay2.API;
using Kursovay2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private SlangAndOldDTO selectSlang;

        public SlangAndOldDTO SelectSlang
        {
            get => selectSlang;
            set
            {
                selectSlang = value;
            }
        }

        public ObservableCollection<SlangAndOldDTO> Slang { get; set; }

        public Library()
        {
            InitializeComponent();
            GetSlangOlds();
            DisplayUserInfo();
        }
        private async void DisplayUserInfo()
        {

            LoginUserDTO login1 = await Client.Instance.GetUser(SingleProfle.user.LoginId);

            if (login1 != null)
            {

                textBlockUserName.Content = login1.LoginName;
            }
            else
            {
                textBlockUserName.Content = "User not found";
            }





        }
        public async void GetSlangOlds()
        {
            List<SlangAndOldDTO> getSlang = await Client.Instance.GetSlangOld();

            if (getSlang != null)
            {
                foreach (SlangAndOldDTO item in getSlang)
                {

                    SlangView.Items.Add(item);

                }
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
