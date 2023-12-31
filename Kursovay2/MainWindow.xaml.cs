﻿using Kursovay2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Kursovay2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ClassWindow
    {
        public MainWindow(): base() 
        {
            InitializeComponent();
        }

        private void PerexodUser(object sender, RoutedEventArgs e)
        {
           
            User.Users user = new User.Users();
            user.Show();
            this.Close();


        }



        private void PerexodAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin();
            admin.Show();
            this.Close();

        }

        private void PerexodGost(object sender, RoutedEventArgs e)
        {

            Gost.Gost gost = new Gost.Gost();
            gost.Show();
            this.Close();

        }

        private void PerehodRegistr(object sender, RoutedEventArgs e)
        {

            Registr.Registr registr = new Registr.Registr();
            registr.Show();
            this.Close();

        }

        private void dragWindows(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
