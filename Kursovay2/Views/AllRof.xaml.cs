﻿using Kursovay2.API;
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
    /// Логика взаимодействия для AllRof.xaml
    /// </summary>
    public partial class AllRof : Window
    {
        private readonly LoginUserDTO user;

        DispatcherTimer timer;
        double panelWidth;
        bool hidden;

        public AllRof()
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
        private void ClickObrat(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(user);
            admin.Show();
            this.Close();
        }

        private void Opisania(object sender, RoutedEventArgs e)
        {

        }

        private void ClickGostWindow(object sender, RoutedEventArgs e)
        {

        }

        private void ClickToMainWindow(object sender, RoutedEventArgs e)
        {

        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {

        }
    }
}
