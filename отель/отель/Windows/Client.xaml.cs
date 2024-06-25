using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using отель.DB;

namespace отель.Windows
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        private List<LoginTbl> list;
        private LoginTbl view;

        public List<LoginTbl> List { get => list; set { list = value; Fill();} }
        public LoginTbl View { get => view; set { view = value; Fill(); } }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Fill([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Client()
        {
            InitializeComponent();
            DataContext = this;
            List = Database.GetInstance().LoginTbls.ToList();
        }
    }
}
