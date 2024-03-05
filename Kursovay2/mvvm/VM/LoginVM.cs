using Kursovay2.API;
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

namespace Kursovay2.mvvm.VM
{
    public class LoginVM:BaseVM
    {
        public string login { get; set; }

        PasswordBox passwordBox;
        internal void RegisterPassBox(PasswordBox passwordBox)
        {
            this.passwordBox = passwordBox;
        }
        public CommandVM SingIn { get; set; }
        public CommandVM SingUp { get; set; }
      
        public LoginVM()
        {

            SingIn = new CommandVM(() =>
            {
                try
                {
                    var user = Client.Instance.UserLogin(login, passwordBox.Password);
                    if (user.Id == 2)
                    {
                        Users users = new Users();
                        users.Show();
                        OnClose();

                    }
                    else if (user.Id == 1)
                    {
                        Admin.Admin adminWindow = new Admin.Admin();
                        adminWindow.Show();
                        OnClose();

                    }
                    else
                    {
                        MessageBox.Show("Логин/пароль неправильный", "Неудачный вход",
                            MessageBoxButton.OK, MessageBoxImage.Error);
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
                OnClose();
            });
        }
   
    }
}
