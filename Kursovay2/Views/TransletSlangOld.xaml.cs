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

namespace Kursovay2.Views
{
    /// <summary>
    /// Логика взаимодействия для TransletSlangOld.xaml
    /// </summary>
    public partial class TransletSlangOld : Window
    {
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        public TransletSlangOld()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
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

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (yourText.Text.Length > 0)
            {
                yourText.ScrollToEnd();
                if (yourText.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length > 250)
                {
                    int index = yourText.Text.LastIndexOf(' ');
                    yourText.Text = yourText.Text.Substring(0, index);
                    yourText.SelectionStart = yourText.Text.Length;
                }
            }

        }

        private void InputTextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {
            if (transletText.Text.Length > 0)
            {
                transletText.ScrollToEnd();
                if (transletText.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length > 250)
                {
                    int index = transletText.Text.LastIndexOf(' ');
                    transletText.Text = transletText.Text.Substring(0, index);
                    transletText.SelectionStart = transletText.Text.Length;
                }
            }

            

        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {

        }

        private async void Trans(object sender, RoutedEventArgs e)
        {
            string textUser = yourText.Text;
            await Client.Instance.TransletOldSlang(textUser);

        
          
            if (textUser != null)
            {

                string textGet = await Client.Instance.TransletOldSlang(textUser);

                if (textGet != null)
                {
                    transletText.Text = textGet;
                }

            }
        }
    }
}
