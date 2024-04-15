using Kursovay2.API;
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

namespace Kursovay2.AddRof
{
    /// <summary>
    /// Логика взаимодействия для AddRof.xaml
    /// </summary>
    public partial class AddRof : Window
    {
        private readonly LoginUserDTO user;
        public RoflDTO SelectRofl { get; set; }

        public AddRof()
        {
            InitializeComponent();
            FillComboBox();
            LoadData();
        }

        private async void FillComboBox()
        {
            List<RoflDTO> comboBoxData = await Client.Instance.GetComboBox();

            if (comboBoxData != null)
            {
                foreach (RoflDTO item in comboBoxData)
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
        private void ClickObratAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin();
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
            Admin.Admin admin = (Admin.Admin)sender;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClickAdminToMainWindow(object sender, RoutedEventArgs e)
        {

        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ResetName(object sender, RoutedEventArgs e)
        {

        }
    }
}
