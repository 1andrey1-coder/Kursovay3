using Kursovay2.API;
using Kursovay2.User;
using Kursovay2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public LoginVM()
        {

            SingIn =new CommandVM(() =>
            {
                try
                {
                    var user = await Client.Instance.UserLogin(login, passwordBox.Password);
                    if (user.RoleId == 2)
                    {
                        Users users = new Users();
                        users.Show();

                    }
                    else if (user.RoleId == 1)
                    {
                        Admin.Admin adminWindow = new Admin.Admin();
                        adminWindow.Show();

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
            

            }
            );
        }
        //private async void SingIn(object sender, RoutedEventArgs e)
        //{
           
//       
        //}
    }
}
