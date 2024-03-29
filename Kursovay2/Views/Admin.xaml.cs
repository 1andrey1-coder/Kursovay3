﻿using Kursovay2.API;
using Kursovay2.Models;
using Kursovay2.User;
using Kursovay2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Kursovay2.Admin
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;

        private readonly LoginUserDTO user;
        public Admin(LoginUserDTO user)
        {
            InitializeComponent();
            this.user = user;

            DisplayUserInfo();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width; 

        }
        private async void DisplayUserInfo()
        {
            LoginUserDTO login1 = await Client.Instance.GetUser(user.LoginId);

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
        private void ClickAddNewRof(object sender, RoutedEventArgs e)
        {

        }

        private void ClickEditRof(object sender, RoutedEventArgs e)
        {

        }

        private void ClickHistoryRof(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClient(object sender, RoutedEventArgs e)
        {
            
            Views.Clients clients = new Views.Clients();
            clients.Show();
            this.Close();
        }

        private void ClickStatuseDone(object sender, RoutedEventArgs e)
        {

        }

        private void ClickStatuseAwaiting(object sender, RoutedEventArgs e)
        {

        }

        private void ClickStatuseInProgress(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCheckAllRof(object sender, RoutedEventArgs e)
        {

        }

        private void ClickAdminToMainWindow(object sender, RoutedEventArgs e)
        {
            
            Admin admin = new Admin(user);
            admin.Show();
            this.Close();

        }

        private void ClickAddRof(object sender, RoutedEventArgs e)
        {
            AddRof.AddRof addRof = new AddRof.AddRof();
            addRof.Show();
            this.Close();

        }

        private void AllRof(object sender, RoutedEventArgs e)
        {
            AllRof allRof = new AllRof();
            allRof.Show();
            this.Close();
        }

        private void ClickRedagturaRof(object sender, RoutedEventArgs e)
        {
            Views.DopRedactor dopRedactor = new Views.DopRedactor();
            dopRedactor.Show();
            this.Close();
        }

        private void ClickToMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
