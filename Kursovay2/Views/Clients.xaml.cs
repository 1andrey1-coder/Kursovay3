using Kursovay2.API;
using Kursovay2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        private readonly LoginUserDTO user;
        DispatcherTimer timer;
        double panelWidth;
        bool hidden;
        private LoginUserDTO selectedClients;

        public LoginUserDTO SelectedClients
        {
            get => selectedClients;
            set
            {
                selectedClients = value;
            }
        }

        public Clients()
        {
            InitializeComponent();
            LoadDefaultImage();
            LoadData();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
        }
        byte[] defaultImage;
        public List<LoginUserDTO> LoginUser { get; set; }
        private void Test()
        {
            var t = new Task(async () =>
            {
                await LoadData();
            });
            t.ContinueWith(s =>
            {
                Dispatcher.Invoke(() =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoginUser)));
                });
            });
            t.Start();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void LoadDefaultImage()
        {
            var stream = Application.GetResourceStream(new Uri("Images\\NotImage.png", UriKind.Relative));
            defaultImage = new byte[stream.Stream.Length];
            stream.Stream.Read(defaultImage, 0, defaultImage.Length);

        }
        private async Task LoadData()
        {
            List<LoginUserDTO> Rofl = await Client.Instance.ListUserAdmin();
            foreach (var d in Rofl)
                if (d.LoginImage == null)
                    d.LoginImage = defaultImage;
            Dispatcher.Invoke(() =>
            {


                if (Rofl != null)
                {

                    AdminListView.ItemsSource = Rofl;

                }
                else
                {
                    MessageBox.Show("Failed to load data from API");
                }
            });
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
        //private void ClickObrat(object sender, RoutedEventArgs e)
        //{
        //    Admin.Admin admin = new Admin.Admin(user);
        //    admin.Show();
        //    this.Close();
        //}

        private void ClickToAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(SingleProfle.user);
            //Admin.Admin admin = new Admin.Admin(user);
            admin.Show();
            this.Close();
        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {
            ClientsPodskaska clientsPodskaska = new ClientsPodskaska();
            clientsPodskaska.Show();
        }

        private void ClickGostWindow(object sender, RoutedEventArgs e)
        {

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

        private void ClickResetRole(object sender, RoutedEventArgs e)
        {
            LoginUserDTO selectedId = (LoginUserDTO)AdminListView.SelectedItem;
            Client.Instance.promote(selectedId);

        }

        private void ClickResetRoleUser(object sender, RoutedEventArgs e)
        {
            LoginUserDTO selectedId = (LoginUserDTO)AdminListView.SelectedItem;
            Client.Instance.demote(selectedId);
            
        }

        private async void TextChanged(object sender, TextChangedEventArgs e)
        {

            string search = myTextBox.Text;
            if (search == "Введите данные")
                search = null;


            if (search != null)
            {

                List<RoflDTO> Rofl = await Client.Instance.SearchApiClients(search);

                if (Rofl != null)
                {
                    AdminListView.ItemsSource = Rofl;
                }
            }
            else
                AdminListView.ItemsSource = await Client.Instance.SearchApi("", "");
        }

        
    }
}
