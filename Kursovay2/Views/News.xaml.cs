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
using System.Windows.Threading;

namespace Kursovay2.Views
{
    /// <summary>
    /// Логика взаимодействия для News.xaml
    /// </summary>
    public partial class News : Window
    {
        private readonly LoginUserDTO user;
        DispatcherTimer timer;
        double panelWidth;
        bool hidden;
        public News()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
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

        private void VernutObrat(object sender, RoutedEventArgs e)
        {
            Users users = new Users(user);
            //Users users = new Users(user);
            users.Show();
            this.Close();
        }

        private void ClickGostWindow(object sender, RoutedEventArgs e)
        {

        }

        private void ClickToAdmin(object sender, RoutedEventArgs e)
        {

            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            Task.Delay(3000).ContinueWith(t =>
            {
                loadingWindow.Dispatcher.Invoke(() =>
                {
                    loadingWindow.Close();

                });
            });

            SingleProfle.user = user;
            //если гость
            if (user.RoleId == 0)
            {
                Gost.Gost gost = new Gost.Gost();
                gost.Show();
                Application.Current.MainWindow.Close();
            }
            //если гость

            if (user.RoleId == 1)
            {

                Admin.Admin adminWindow = new Admin.Admin(SingleProfle.user);
                adminWindow.Show();
                Application.Current.MainWindow.Close();
            }
            if (user.RoleId == 2)
            {
                Users userWindow = new Users(user);
                userWindow.Show();
                Application.Current.MainWindow.Close();
            }
            

            
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
        private void ResetSearch(object sender, RoutedEventArgs e)
        {

        }
        private void Focus(object sender, RoutedEventArgs e)
        {
            if (myTextBox.Text == "Введите данные")
            {
                myTextBox.Text = "";
                myTextBox.Foreground = Brushes.Gray;
            }
        }

        private void lastFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(myTextBox.Text))
            {
                myTextBox.Text = "Введите данные";
                myTextBox.Foreground = Brushes.Gray;
            }
        }
    }
}
