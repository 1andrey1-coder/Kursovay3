using Kursovay2.API;
using Kursovay2.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для DopRedactor.xaml
    /// </summary>
    public partial class DopRedactor : Window
    {
        DispatcherTimer timer;

        double panelWidth;
        bool hidden;
        private RoflDTO selectRofl = new RoflDTO();
        private int selectedId;
        private readonly LoginUserDTO user;

        
        public RoflDTO SelectRofl
        {
            get => selectRofl; 
            set
            { 
               selectRofl = value; 
            }
        }

        //public static readonly DependencyProperty SelectedIdProperty = DependencyProperty.Register("SelectedId", typeof(int), typeof(Admin));

        //public int SelectedId
        //{
        //    get { return (int)GetValue(SelectedIdProperty); }
        //    set { SetValue(SelectedIdProperty, value); }
        //}

        public DopRedactor(RoflDTO selectedId) 
        {
            InitializeComponent();
            DataContext = this;
            //SelectRofl = selectRofl;

            SelectRofl = selectedId;
            if (selectedId == null)
                selectedId = new RoflDTO();
            SelectRofl = selectedId;

            Signal(nameof(SelectRofl));

            StatusComboBox();
            EndComboBox();
            TegComboBox();
            GenreComboBox();
            StartComboBox();

            LoadData();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 0);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
        }


        private void ClickObratAddRof(object sender, RoutedEventArgs e)
        {
            AddRof.AddRof addRof = new AddRof.AddRof();
            addRof.Show();
            this.Close();
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {

        }

        private void ClickDelete(object sender, RoutedEventArgs e)
        {

        }

        private void AdminWindow(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(SingleProfle.user);
            //Admin.Admin admin = new Admin.Admin(user);
            admin.Show();
            Close();
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
            Admin.Admin admin = new Admin.Admin(SingleProfle.user);
            admin.Show();
            Close();
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

        private void ClickDeleteRof(object sender, RoutedEventArgs e)
        {

        }

        private void ClickStatuseAwaiting(object sender, RoutedEventArgs e)
        {

        }

        private void ClickStatuseDone(object sender, RoutedEventArgs e)
        {

        }

        private void ClickStatuseInProgress(object sender, RoutedEventArgs e)
        {

        }

        private void ClickRedagturaRof(object sender, RoutedEventArgs e)
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

        private void ClickToAdmin(object sender, RoutedEventArgs e)
        {
            Admin.Admin admin = new Admin.Admin(SingleProfle.user);
            admin.Show();
            this.Close();
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
        private async void GenreComboBox()
        {
            List<GenreDTO> comboBoxData = await Client.Instance.GetComboBoxGenre();

            if (comboBoxData != null)
            {
                foreach (GenreDTO item in comboBoxData)
                {

                    AdminComboBoxGenre.Items.Add(item);

                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void AddPhoto(object sender, RoutedEventArgs e)
        {
            string dir = Environment.CurrentDirectory + @"\Images\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images|*.jpg;";
            if (dlg.ShowDialog() == true)
            {
                var test = new BitmapImage(new Uri(dlg.FileName));
                if (test.PixelWidth > 2000 || test.PixelHeight > 2000)
                {
                    MessageBox.Show("Картинка слишком большая");
                    return;
                }
                string newFile = dir + new FileInfo(dlg.FileName).Name;
                File.Copy(dlg.FileName, newFile, true);
                SelectRofl.RoflImage = File.ReadAllBytes(newFile);
                Signal("SelectedRofl");
            }
        }

        private async void PutName(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.Show();

            Task.Delay(3000).ContinueWith(t =>
            {
                loadingWindow.Dispatcher.Invoke(() =>
                {
                    loadingWindow.Close();

                });
            });
            SelectRofl.RoflId = SelectRofl.RoflId;
            SelectRofl.RoflMinOpisanie = AddMinOpisania.Text;
            SelectRofl.RoflOpisanie = AddOpisania.Text;
            SelectRofl.RoflName = AddNameRofl.Text;
            //SelectRofl.TegId = SelectRofl.TegId;
            //SelectRofl.RoflStatusId = SelectRofl.RoflStatusId;   
            //SelectRofl.RoflEndId = SelectRofl.RoflEndId;
            //SelectRofl.RoflStartId = SelectRofl.RoflStartId;
            //SelectRofl.RoflGenreId = SelectRofl.RoflGenreId;




            //string miniopis = AddMinOpisania.Text;
            //string opis = AddOpisania.Text;
            //string Name = AddNameRofl.Text;
            StatusDTO statusId = (StatusDTO)AdminComboBoxStatus.SelectedItem;
            EndDTO endId = (EndDTO)AdminComboBoxEnd.SelectedItem;
            TegDTO tegId = (TegDTO)AdminComboBoxTeg.SelectedItem;
            StartDTO startId = (StartDTO)AdminComboBoxStart.SelectedItem;
            GenreDTO genreId = (GenreDTO)AdminComboBoxGenre.SelectedItem;
            //miniopis != null &&
            if (selectedId != null )
            {

            if (statusId != null && endId != null &&
                   tegId != null && startId != null && genreId != null)
            {
                //int selectedStatusId =  statusId.StatusId;
                await Client.Instance.SendUserPutData(new RoflDTO
                {
                    RoflId = SelectRofl.RoflId,
                    TegId = tegId.TegId,
                    RoflStartId = startId.StartId,
                    RoflStatusId = statusId.StatusId,
                    RoflGenreId = genreId.GenreId,
                    RoflEndId = endId.EndId,
                    RoflName = SelectRofl.RoflName,
                    RoflMinOpisanie = SelectRofl.RoflMinOpisanie,
                    RoflOpisanie = SelectRofl.RoflOpisanie,
                    RoflImage = SelectRofl.RoflImage,
                });
            }
                LoadData();
            }
        }

        private void Podskaska(object sender, RoutedEventArgs e)
        {

        }
    }
}
