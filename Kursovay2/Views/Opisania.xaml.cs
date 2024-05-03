using Kursovay2.API;
using Kursovay2.Models;
using Kursovay2.User;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Kursovay2.Views
{
    /// <summary>
    /// Логика взаимодействия для Opisania.xaml
    /// </summary>
    public partial class Opisania : Window
    {
        //дописать заместо LoginUserDTO, вставить таблицу RoflDTO
        //private readonly LoginUserDTO user;
        DispatcherTimer timer;
        double panelWidth;
        bool hidden;
        private RoflDTO selectedRofl;

        public RoflDTO SelectedRofl
        {
            get => selectedRofl;
            set
            {
                selectedRofl = value;

            }
        }
        public Opisania(RoflDTO selectedItem)
        {
            InitializeComponent();
            SelectedRofl = selectedItem;
            DataRofl(selectedItem);
            DisplayRoflInfo();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
        }

        private async void DataRofl(RoflDTO selectedItem)
        {
            if (selectedItem != null)
            {
                AddOpisania.Text = selectedItem.RoflOpisanie;
            }
            else
            {
                MessageBox.Show("Ошибка загрузки описания");
            }
        }


        private async void DisplayRoflInfo()
        {

            LoginUserDTO login1 = await Client.Instance.GetRofl(SingleProfle.user.LoginId);

            if (login1 != null)
            {

                textBlockUserName.Content = login1.LoginName;

            }
            else
            {
                textBlockUserName.Content = "User not found";
            }





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
       
        private void ClickToAdmin(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            Task.Delay(1500).ContinueWith(t =>
            {
                loadingWindow.Dispatcher.Invoke(() =>
                {
                    loadingWindow.Close();

                });
            });


            if (SingleProfle.user.RoleId == 1)
            {

                Admin.Admin adminWindow = new Admin.Admin(SingleProfle.user);
                adminWindow.Show();
                Close();
            }
            if (SingleProfle.user.RoleId == 2)
            {
                User.Users userWindow = new User.Users(SingleProfle.user);
                userWindow.Show();
                Close();
            }
            //смотрит если роль админ то в админку если клиент то в клиента
        }

        private void ClickGostWindow(object sender, RoutedEventArgs e)
        {

        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {
            OpisaniaPodskaska opisaniaPodskaska = new OpisaniaPodskaska();
            opisaniaPodskaska.Show();
        }
        private void ClickObrat(object sender, RoutedEventArgs e)
        {
            AddRof.AddRof addRof = new AddRof.AddRof();
            addRof.Show();
            this.Close();
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
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            //Window parentWindow = Window.GetWindow(this);
            //parentWindow.WindowState = WindowState.Maximized;
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
        private void Focus(object sender, RoutedEventArgs e)
        {
            if (myTextBox.Text == "Введите данные")
            {
                myTextBox.Text = "";
                myTextBox.Foreground = Brushes.Gray;
            }
        }

        private void lastFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(myTextBox.Text))
            {
                myTextBox.Text = "Введите данные";
                myTextBox.Foreground = Brushes.Gray;
            }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AddOpisania.Text.Length > 0)
            {
                AddOpisania.ScrollToEnd();
                if (AddOpisania.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length > 250)
                {
                    int index = AddOpisania.Text.LastIndexOf(' ');
                    AddOpisania.Text = AddOpisania.Text.Substring(0, index);
                    AddOpisania.SelectionStart = AddOpisania.Text.Length;
                }
            }
        }
    }
}
