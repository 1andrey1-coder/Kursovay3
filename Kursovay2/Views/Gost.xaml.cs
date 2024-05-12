using Kursovay2.API;
using Kursovay2.Models;
using Kursovay2.User;
using Kursovay2.Views;
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
using System.Windows.Threading;

namespace Kursovay2.Gost
{
    /// <summary>
    /// Логика взаимодействия для Gost.xaml
    /// </summary>
    public partial class Gost : Window
    {
        
        DispatcherTimer timer;
        double panelWidth;
        bool hidden;
        public Gost() 
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                sidePanel.Width += 1;
                if (sidePanel.Width >= panelWidth)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= 1;
                if (sidePanel.Width <= 35)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void ClickGostWindow(object sender, RoutedEventArgs e)
        {
            Gost gost = new Gost();
            gost.Show();
            this.Close();
        }

        private void ClickToMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ClickOpisaniaRof(object sender, RoutedEventArgs e)
        {

        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {
            GostPodskaska gostPodskaska = new GostPodskaska();
            gostPodskaska.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.WindowState = WindowState.Minimized;
        }

        //private void ClickNew(object sender, RoutedEventArgs e)
        //{
            
        //    News news = new News();
        //    news.Show();
        //    this.Close();
        //}
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
           
            if (WindowState == WindowState.Normal)
            {
                // Меняем размер окна на максимальный
                WindowState = WindowState.Maximized;
            }
            else
            {
                // Меняем размер окна на обычный
                WindowState = WindowState.Normal;
            }


        }
        private void ResetSearch(object sender, RoutedEventArgs e)
        {

        }
        private async void Focus(object sender, RoutedEventArgs e)
        {


            if (myTextBox.Text == "Введите данные")
            {
                myTextBox.Text = "";
                myTextBox.Foreground = Brushes.Black;

            }
        }

        private async void lastFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(myTextBox.Text))
            {
                myTextBox.Text = "Введите данные";
                myTextBox.Foreground = Brushes.Gray;
            }

        }
        private async void TextChanged(object sender, TextChangedEventArgs e)
        {



            string search = myTextBox.Text;
            string comboboxTeg = AdminComboBoxTeg.Text;
            if (search == "Введите данные")
                search = null;


            if (search != null)
            {

                List<RoflDTO> Rofl = await Client.Instance.SearchApi(search, comboboxTeg);

                if (Rofl != null)
                {
                    AdminListView.ItemsSource = Rofl;
                }
            }
            else
                AdminListView.ItemsSource = await Client.Instance.SearchApi("", "");
        }

        private void translet(object sender, RoutedEventArgs e)
        {
            TransletSlangOld transletSlangOld = new TransletSlangOld();
                transletSlangOld.Show();
                Close();
            
        }
    }
}
