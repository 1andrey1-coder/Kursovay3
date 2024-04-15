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
using System.Windows.Threading;

namespace Kursovay2.User
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        DispatcherTimer timer;
        double panelWidth;
        bool hidden;
        public Users()
        {
            InitializeComponent();

            DisplayUserInfo();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;

        }
        private async void DisplayUserInfo()
        {
            LoginUserDTO login1 = await Client.Instance.GetUser(SingleProfle.user.LoginId);

            if (login1 != null)
            {

                labelUser.Content = login1.LoginName;
            }
            else
            {
                labelUser.Content = "User not found";
            }





        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                sidePanel.Width += 1;
                if (sidePanel.Width >= panelWidth)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= 1;
                if (sidePanel.Width <= 35)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void ClickUserToMainWindow(object sender, RoutedEventArgs e)
        {
          
            User.Users user2 = new User.Users();
            //User.Users user2 = new User.Users(user);
            user2.Show();
            this.Close();
        }


        private void ClickLokInf(object sender, RoutedEventArgs e)
        {
            Views.LokInf lokInf = new Views.LokInf();
            lokInf.Show();
            this.Close();
        }

        private void ClickHistoryRole(object sender, RoutedEventArgs e)
        {
            Views.HistoryRole historyRole = new Views.HistoryRole();
            historyRole.Show();
            this.Close();
        }

        private void ClickAdmini(object sender, RoutedEventArgs e)
        {
            Views.Admini_We_ admini_We_ = new Views.Admini_We_();
            admini_We_.Show();
            this.Close();
        }

        //private void ClickGey(object sender, RoutedEventArgs e)
        //{
        //    Views.Gey gey = new Views.Gey();
        //    gey.Show();
        //    this.Close();
        //}

        //private void ClickNewMemNow(object sender, RoutedEventArgs e)
        //{
        //    Views.NewMemNow newMemNow = new Views.NewMemNow();
        //    newMemNow.Show();
        //    this.Close();
        //}

        private void ClickNews(object sender, RoutedEventArgs e)
        {
            Views.News news = new Views.News();
            news.Show();
            this.Close();
        }

        //private void ClickNostal(object sender, RoutedEventArgs e)
        //{
        //    Views.Nostal nostal = new Views.Nostal();
        //    nostal.Show();
        //    this.Close();
        //}

        private void ClickMebl(object sender, RoutedEventArgs e)
        {

        }

        private void ClickToMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow admin = new MainWindow();
            admin.Show();
            this.Close();
        }

        private void ClickOpisaniaRof(object sender, RoutedEventArgs e)
        {

        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {

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

        private void AllRof(object sender, RoutedEventArgs e)
        {

        }
    }
}
