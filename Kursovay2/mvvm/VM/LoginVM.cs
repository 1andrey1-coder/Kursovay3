﻿using Kursovay2.API;
using Kursovay2.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Kursovay2.mvvm;
using Kursovay2.Views;
using Kursovay2.Models;

namespace Kursovay2.mvvm.VM
{
    public class LoginVM:BaseVM
    {
        public string mail { get; set; }
        PasswordBox passwordBox;
        internal void RegisterPassBox(PasswordBox passwordBox)
        {
            this.passwordBox = passwordBox;
        }
        public CommandVM SingIn { get; set; }
        public CommandVM SingUp { get; set; }
        public LoginVM()
        {
            SingIn = new CommandVM(async () =>
            {
                try
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
                    var user = await Client.Instance.UserLogin(passwordBox.Password, mail);
                    SingleProfle.User = user;
                    if (user.RoleId == 1)
                    {
                        Admin.Admin adminWindow = new Admin.Admin(user);
                        adminWindow.Show();
                        Application.Current.MainWindow.Close();
                    }
                    if  (user.RoleId == 2)
                    {
                        Users userWindow = new Users(user);
                        userWindow.Show();
                        Application.Current.MainWindow.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            SingUp = new CommandVM(() =>
            {
                Registr.Registr registr = new Registr.Registr();
                registr.Show();
                Application.Current.MainWindow.Close();
            });
        }
   
    }
}
