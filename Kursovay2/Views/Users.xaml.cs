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
      


        private void ClickLokInf(object sender, RoutedEventArgs e)
        {

        }


        private void ClickUserToMainWindow(object sender, RoutedEventArgs e)
        {
          
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ClickHistoryRole(object sender, RoutedEventArgs e)
        {

        }

        private void ClickAdmini(object sender, RoutedEventArgs e)
        {

        }

        private void ClickGey(object sender, RoutedEventArgs e)
        {

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
