using Kursovay2.API;
using Kursovay2.Models;
using Kursovay2.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Kursovay2.AddRof
{
    /// <summary>
    /// Логика взаимодействия для AddRof.xaml
    /// </summary>
    public partial class AddRof : Window
    {
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;

        private readonly LoginUserDTO user;
        private RoflDTO selectTeg;

        public StatusDTO SelectStatus { get; set; }
       


        public AddRof()
        {
            InitializeComponent();
            StatusComboBox();
            EndComboBox();
            TegComboBox();
            StartComboBox();
            LoadData();
           
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

        private async void StatusComboBox()
        {
            List<StatusDTO> comboBoxData = await Client.Instance.GetComboBoxStatus();

            if (comboBoxData != null)
            {
                foreach (StatusDTO item in comboBoxData)
                {

                    AdminComboBoxStatus.Items.Add(item);
                   
                }
            }
        }
        private async void EndComboBox()
        {
            List<EndDTO> comboBoxData = await Client.Instance.GetComboBoxEnd();

            if (comboBoxData != null)
            {
                foreach (EndDTO item in comboBoxData)
                {

                    AdminComboBoxEnd.Items.Add(item);

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
        private async void StartComboBox()
        {
            List<StartDTO> comboBoxData = await Client.Instance.GetComboBoxStart();

            if (comboBoxData != null)
            {
                foreach (StartDTO item in comboBoxData)
                {

                    AdminComboBoxStart.Items.Add(item);

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

        private async void AddName(object sender, RoutedEventArgs e)
        {
            string miniopis = AddMinOpisania.Text;
            string Name = AddNameRofl.Text;
            StatusDTO statusId = (StatusDTO)AdminComboBoxStatus.SelectedItem;
            EndDTO endId = (EndDTO)AdminComboBoxEnd.SelectedItem;
            TegDTO tegId = (TegDTO)AdminComboBoxTeg.SelectedItem;
            StartDTO startId = (StartDTO)AdminComboBoxStart.SelectedItem;
            //miniopis != null &&
            if (statusId != null && endId != null && tegId != null && startId !=null )
            {
                int selectedStatusId =  statusId.StatusId;
                await Client.Instance.SendUserData(selectedStatusId, miniopis, Name);
            }
            LoadData();
            


        }

        private void ClickObratAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(SingleProfle.user);
            //Admin.Admin admin = new Admin.Admin(user);
            admin.Show();
            this.Close();
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {

        }

        private void ClickDopRedactor(object sender, RoutedEventArgs e)
        {
            Views.DopRedactor dopRedactor = new Views.DopRedactor();
            dopRedactor.Show();
            this.Close();

        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {

        }

        private void AddRof2(object sender, RoutedEventArgs e)
        {

        }

        private void Opisania(object sender, RoutedEventArgs e)
        {
            Views.Opisania opisania = new Views.Opisania();
            opisania.Show();
            this.Close();
        }

        private void ClickToAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(SingleProfle.user);
            admin.Show();
            this.Close();
        }

        private void Podskaska(object sender, RoutedEventArgs e)
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

        private void ClickAdminToMainWindow(object sender, RoutedEventArgs e)
        {

        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {

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
    }
}

