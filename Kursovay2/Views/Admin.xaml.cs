using Kursovay2.API;
using Kursovay2.Models;
using Kursovay2.User;
using Kursovay2.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace Kursovay2.Admin
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        private RoflDTO selectRofl;
        private int selectedId;


        public ObservableCollection<RoflDTO> Rofl { get; set; }

        public RoflDTO SelectRofl
        {
            get => selectRofl;
            set
            {
                selectRofl = value;
            }
        }

        public Admin(LoginUserDTO user)
        {
            InitializeComponent();
            //для отображения данных
            GenreComboBox();
            TegComboBox();
            LoadData();
            //SearchApi();



            DisplayUserInfo();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;

        }

        private async void GenreComboBox()
        {
            List<GenreDTO> comboBoxData = await Client.Instance.GetComboBoxGenre();

            if (comboBoxData != null)
            {
                foreach (GenreDTO item in comboBoxData)
                {

                    //AdminComboBoxGenre.Items.Add(item);

                }
            }
        }
        private async void TegComboBox()
        {
            List<TegDTO> comboBoxData = await Client.Instance.GetComboBoxTeg();

            if (comboBoxData != null)
            {
                foreach (TegDTO item in comboBoxData)
                {

                    AdminComboBoxTeg.Items.Add(item);

                }
            }
        }

        private async void LoadData()
        {
            List<RoflDTO> Rofl = await Client.Instance.GetListRofl();

            if (Rofl != null)
            {
                AdminListView.ItemsSource = Rofl;
            }
            else
            {
                MessageBox.Show("Failed to load data from API");
            }
        }


        private async void DisplayUserInfo()
        {

            LoginUserDTO login1 = await Client.Instance.GetUser(SingleProfle.user.LoginId);

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


        private void ClickAdminToMainWindow(object sender, RoutedEventArgs e)
        {

            Admin admin = new Admin(SingleProfle.user);
            admin.Show();
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
            if (SingleProfle.user.RoleId == 1)
            {

                AllRof allRof = new AllRof();
                allRof.Show();
                this.Close();
            }
            if (SingleProfle.user.RoleId == 2)
            {
                AllRof allRof = new AllRof();
                allRof.Show();
                this.Close();
            }
            //AllRof allRof = new AllRof();
            //allRof.Show();
            //this.Close();
        }





        private void ClickRedagturaRof(object sender, RoutedEventArgs e)
        {
            RoflDTO selectedId = (RoflDTO)AdminListView.SelectedItem;

            //RoflDTO selectedRofl = (RoflDTO)AdminListView.SelectedItem;
            Views.DopRedactor dopRedactor = new Views.DopRedactor(selectedId);
            //dopRedactor.SelectedId = selectedId;
            dopRedactor.Show();
            this.Close();
        }

        private void ClickToMainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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

        private async void ClickDeleteRof(object sender, RoutedEventArgs e)
        {
            try
            {

                RoflDTO deleteid = (RoflDTO)AdminListView.SelectedItem;
                if (deleteid != null)
                {
                    int selectedDataId = deleteid.RoflId;
                    await Client.Instance.DeleteDataAsync(selectedDataId);
                    MessageBox.Show("Данные успешно удалены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении данных:{ex.Message}", "Неудача", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AllUser(object sender, RoutedEventArgs e)
        {
            Views.Clients clients = new Views.Clients();
            clients.Show();
            this.Close();
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

        private void Profile(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            Task.Delay(1000).ContinueWith(t =>
            {
                loadingWindow.Dispatcher.Invoke(() =>
                {
                    loadingWindow.Close();

                });
            });

            if (SingleProfle.user.RoleId == 1)
            {

                ProfileAdmin adminWindow = new ProfileAdmin();
                adminWindow.Show();
                Close();
            }
            if (SingleProfle.user.RoleId == 2)
            {
                ProfileUser userWindow = new ProfileUser();
                userWindow.Show();
                Close();
            }


        }


        private async void ResetSearch(object sender, RoutedEventArgs e)
        {
            //myTextBox.Text = string.Empty;
            AdminComboBoxTeg.SelectedIndex = -1;
            //AdminComboBoxGenre .SelectedIndex = -1;



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

        private void library(object sender, RoutedEventArgs e)
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

                Library adminWindow = new Library();
                adminWindow.Show();
                Close();
            }
            if (SingleProfle.user.RoleId == 2)
            {
                Library userWindow = new Library();
                userWindow.Show();
                Close();
            }
        }

        private async void TextChanged(object sender, TextChangedEventArgs e)
        {



            string search = myTextBox.Text;
            string comboboxTeg = AdminComboBoxTeg.Text;

            await Client.Instance.SearchApi(search, comboboxTeg);

            if (search != null)
            {

                List<RoflDTO> Rofl = await Client.Instance.SearchApi(search, comboboxTeg);

                if (Rofl != null)
                {
                    AdminListView.ItemsSource = Rofl;
                }
               

                   
                
            }

        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {
            AdminPodskaska adminPodskaska = new AdminPodskaska();
            adminPodskaska.Show();
        }

        private void ClickOpisania(object sender, RoutedEventArgs e)
        {
            RoflDTO selectedItem = (RoflDTO)AdminListView.SelectedItem;
            Opisania opisania = new Opisania(selectedItem);
            opisania.Show();

        }

        private void translet(object sender, RoutedEventArgs e)
        {
            TransletSlangOld transletSlangOld = new TransletSlangOld();
            transletSlangOld.Show();

        }
    }
}
