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
            
            Views.Clients clients = new Views.Clients();
            clients.Show();
            this.Close();
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
            
            MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            this.Close();

        }

        private void ClickAddRof(object sender, RoutedEventArgs e)
        {
            AddRof.AddRof addRof = new AddRof.AddRof();
            addRof.Show();
            this.Close();

        }

        private void AllRof(object sender, RoutedEventArgs e)
        {
            Views.AllRof allRof = new Views.AllRof();
            allRof.Show();
            this.Close();
        }

        private void ClickRedagturaRof(object sender, RoutedEventArgs e)
        {
            Views.DopRedactor dopRedactor = new Views.DopRedactor();
            dopRedactor.Show();
            this.Close();
        }
    }
}
