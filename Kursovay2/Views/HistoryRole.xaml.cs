using Kursovay2.API;
using Kursovay2.Models;
using Kursovay2.User;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
    /// Логика взаимодействия для HistoryRole.xaml
    /// </summary>
    public partial class HistoryRole : Window
    {
        private readonly LoginUserDTO user;
        DispatcherTimer timer;
        double panelWidth;
        bool hidden;

        public HistoryRole()
        {
            InitializeComponent();
            LoadDefaultImage();
            LoadData();
            DisplayUserInfo();

            datePicker.SelectedDate = DateTime.Now;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
        }

        private async void LoadData()
        {
            List<RoflDTO> Rofl = await Client.Instance.GetListRofl();
            foreach (var d in Rofl)
                if (d.RoflImage == null)
                    d.RoflImage = defaultImage;
            Dispatcher.Invoke(() =>
            {
                if (Rofl != null)
                {
                    AdminListView.ItemsSource = Rofl;
                }
                else
                {
                    MessageBox.Show("Failed to load data from API");
                }
            });
        }

        private async void DisplayUserInfo()
        {

            LoginUserDTO login1 = await Client.Instance.GetUser(SingleProfle.User.LoginId);

            if (login1 != null)
            {

                textBlockUserName.Content = login1.LoginName;
            }
            else
            {
                textBlockUserName.Content = "User not found";
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

        private void VernutObrat(object sender, RoutedEventArgs e)
        {
            Users users = new Users(user);
            //Users users = new Users(user);
            users.Show();
            this.Close();
        }

        private void ClickToAdmin(object sender, RoutedEventArgs e)
        {
            //смотрит если роль админ то в админку если клиент то в клиента

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

        private void Podskaska(object sender, RoutedEventArgs e)
        {
            HistoryRolePodskaska historyRolePodskaska = new HistoryRolePodskaska();
            historyRolePodskaska.Show();
        }

        private void ClickGostWindow(object sender, RoutedEventArgs e)
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

        private async void Focus(object sender, RoutedEventArgs e)
        {


            if (myTextBox.Text == "Введите данные")
            {
                myTextBox.Text = "";
                myTextBox.Foreground = Brushes.Black;

            }
        }

        private async void lastFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(myTextBox.Text))
            {
                myTextBox.Text = "Введите данные";
                myTextBox.Foreground = Brushes.Gray;
            }

        }
        byte[] defaultImage;

        private void LoadDefaultImage()
        {
            var stream = Application.GetResourceStream(new Uri("Images\\NotImage.png", UriKind.Relative));
            defaultImage = new byte[stream.Stream.Length];
            stream.Stream.Read(defaultImage, 0, defaultImage.Length);

        }
        private async void TextChanged(object sender, TextChangedEventArgs e)
        {



            string search = myTextBox.Text;
            if (search == "Введите данные")
                search = null;


            if (search != null)
            {

                List<RoflDTO> Rofl = await Client.Instance.SearchApiNotComboBox(search);

                foreach (var d in Rofl)
                    if (d.RoflImage == null)
                        d.RoflImage = defaultImage;
                Dispatcher.Invoke(() =>
                {
                    if (Rofl != null)
                    {
                        AdminListView.ItemsSource = Rofl;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load data from API");
                    }
                });

            }
            else
                //AdminListView.ItemsSource = await Client.Instance.SearchApiNotComboBox("");
                LoadData();
        }
        private async void DatePickerChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            

        }

        private void SearchDate(object sender, RoutedEventArgs e)
        {

        }

        private async void Searchte(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.Now;
            await Client.Instance.DateTimePicker(selectedDate);

            List<RoflDTO> Rofl = await Client.Instance.DateTimePicker(selectedDate);
            if (selectedDate != null)
            {
                AdminListView.ItemsSource = Rofl;

            }
        }
    }
}
