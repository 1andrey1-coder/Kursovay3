using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using отель.DB;

namespace отель.Windows
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private LoginTbl log1;

        public Register()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Fill([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public LoginTbl log { get => log1; set { log1 = value; Fill(); } }

        private void AddClietns(object sender, RoutedEventArgs e)
        {
           
            

            using (var db = new _3kursContext())
            {
                var log = new LoginTbl();
                log.Login = nametxt.Text;
                log.Password = passwordtxt.Text;
                db.LoginTbls.Add(log);
                db.SaveChanges();
                    



            }
        }
    }
}
