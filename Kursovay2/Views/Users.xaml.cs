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

namespace Kursovay2.User
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();  
        }

        private void ClickUserToMainWindow(object sender, RoutedEventArgs e)
        {
          
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void ClickLokInf(object sender, RoutedEventArgs e)
        {
            Views.LokInf lokInf = new Views.LokInf();
            lokInf.Show();
            this.Close();
        }

        private void ClickHistoryRole(object sender, RoutedEventArgs e)
        {
            Views.HistoryRole historyRole = new Views.HistoryRole();
            historyRole.Show();
            this.Close();
        }

        private void ClickAdmini(object sender, RoutedEventArgs e)
        {
            Views.Admini_We_ admini_We_ = new Views.Admini_We_();
            admini_We_.Show();
            this.Close();
        }

        private void ClickGey(object sender, RoutedEventArgs e)
        {
            Views.Gey gey = new Views.Gey();
            gey.Show();
            this.Close();
        }

        private void ClickNewMemNow(object sender, RoutedEventArgs e)
        {
            Views.NewMemNow newMemNow = new Views.NewMemNow();
            newMemNow.Show();
            this.Close();
        }

        private void ClickNews(object sender, RoutedEventArgs e)
        {
            Views.News news = new Views.News();
            news.Show();
            this.Close();
        }

        private void ClickNostal(object sender, RoutedEventArgs e)
        {
            Views.Nostal nostal = new Views.Nostal();
            nostal.Show();
            this.Close();
        }

        private void ClickMebl(object sender, RoutedEventArgs e)
        {

        }



    }
}
