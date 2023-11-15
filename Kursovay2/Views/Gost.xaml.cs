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

namespace Kursovay2.Gost
{
    /// <summary>
    /// Логика взаимодействия для Gost.xaml
    /// </summary>
    public partial class Gost : Window
    {
        public Gost()
        {
            InitializeComponent();
        }

        private void ClickMem(object sender, RoutedEventArgs e)
        {

        }

        private void ClickNews(object sender, RoutedEventArgs e)
        {

        }

        private void ClickNostalGame(object sender, RoutedEventArgs e)
        {

        }

        private void ClickMebl(object sender, RoutedEventArgs e)
        {

        }

        private void ClickModno(object sender, RoutedEventArgs e)
        {

        }

        private void ClickNoModno(object sender, RoutedEventArgs e)
        {

        }

        private void ClickLegend(object sender, RoutedEventArgs e)
        {

        }

        private void ClickGostToMainWindow(object sender, RoutedEventArgs e)
        {
           
            MainWindow mainWindow  = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
