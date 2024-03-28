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



        private void ClickModno(object sender, RoutedEventArgs e)
        {
            Views.Modno modno = new Views.Modno();
            modno.Show();
            this.Close();
        }

        private void ClickNoModno(object sender, RoutedEventArgs e)
        {
            Views.NoModno modnono = new Views.NoModno();
            modnono.Show();
            this.Close();
        }

        private void ClickLegend(object sender, RoutedEventArgs e)
        {
            Views.Legend legend = new Views.Legend();
            legend.Show();
            this.Close();
        }

        private void ClickGostToMainWindow(object sender, RoutedEventArgs e)
        {
           
            MainWindow mainWindow  = new MainWindow();
            //mainWindow.Show();
            this.Close();
        }

        //private void ClickNewMemNow(object sender, RoutedEventArgs e)
        //{
        //    Views.NewMemNow newMemNow = new Views.NewMemNow();
        //    newMemNow.Show();
        //    this.Close();
        //}

        //private void ClickNewsGost(object sender, RoutedEventArgs e)
        //{
        //    Views.NewsGost news = new Views.NewsGost();
        //    news.Show();
        //    this.Close();
        //}

        private void ClickNostal(object sender, RoutedEventArgs e)
        {
            Views.Nostal nostal = new Views.Nostal();
            nostal.Show();
            this.Close();
        }

        private void ClickMebl(object sender, RoutedEventArgs e)
        {

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

        }
    }
}
