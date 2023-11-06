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

namespace Kursovay2.Admin
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
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
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void ClickAddRof(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddRof.AddRof addRof = new AddRof.AddRof();
            addRof.Show();
        }
    }
}
