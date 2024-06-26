﻿using Kursovay2.API;
using Kursovay2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для AllRof.xaml
    /// </summary>
    public partial class AllRof : Window
    {
        private readonly LoginUserDTO user;

        DispatcherTimer timer;
        double panelWidth;
        bool hidden;
        private RoflDTO selectedRofl = new RoflDTO();
        public RoflDTO SelectedRofl
        {
            get => selectedRofl;
            set
            {
                selectedRofl = value;
            }
        }


        public AllRof()
        {
            InitializeComponent();
            DisplayUserInfo();
            LoadDefaultImage();
            LoadData();
            Test();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
        }

        private void Test()
        {
            var t = new Task(async () =>
            {
                await LoadData();
            });
            t.ContinueWith(s =>
            {
                Dispatcher.Invoke(() =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rofl)));
                });
            });
            t.Start();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<RoflDTO> Rofl { get; set; }

        byte[] defaultImage;

        private void LoadDefaultImage()
        {
            var stream = Application.GetResourceStream(new Uri("Images\\NotImage.png", UriKind.Relative));
            defaultImage = new byte[stream.Stream.Length];
            stream.Stream.Read(defaultImage, 0, defaultImage.Length);

        }
        private async Task LoadData()
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
        private void ClickObrat(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(SingleProfle.User);
            //Admin.Admin admin = new Admin.Admin(user);
            admin.Show();
            this.Close();
        }

        private void Opisania(object sender, RoutedEventArgs e)
        {

        }

        private void ClickToAdminWindow(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(SingleProfle.User);
            admin.Show();
            this.Close();
        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {
            //AddRofPodskaska addRofPodskaska = new AddRofPodskaska();
            //addRofPodskaska.Show();
            AddRofPodskaska addRofPodskaska = new AddRofPodskaska();
            addRofPodskaska.Show();
            
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

        private void ClickToAdmiWindow(object sender, RoutedEventArgs e)
        {
            if (SingleProfle.User.RoleId == 1)
            {

                Admin.Admin adminWindow = new Admin.Admin(SingleProfle.User);
                adminWindow.Show();
                Close();
            }
            if (SingleProfle.User.RoleId == 2)
            {
                User.Users userWindow = new User.Users(user);
                userWindow.Show();
                Close();
            }

            //Admin.Admin admin = new Admin.Admin(SingleProfle.user);
            //admin.Show();
            //this.Close();
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
    }
}
