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
        private void ClickMem(object sender, RoutedEventArgs e)
        {

        }

        private void ClickNews(object sender, RoutedEventArgs e)
        {

        }

        private void ClickNostal(object sender, RoutedEventArgs e)
        {

        }

        private void ClickMebl(object sender, RoutedEventArgs e)
        {

        }

        private void ClickLokInf(object sender, RoutedEventArgs e)
        {

        }

        private void ClickRole(object sender, RoutedEventArgs e)
        {

        }

        private void ClickUserToMainWindow(object sender, RoutedEventArgs e)
        {
          
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
