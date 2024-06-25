using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using отель.DB;
using отель.Windows;

namespace отель
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public LoginTbl LoginTbl {  get; set; }

        private void Dxod(object sender, RoutedEventArgs e)
        {
            var loginTbl = Database.GetInstance().LoginTbls.FirstOrDefault(s=>s.Login == logintxt.Text &&
            s.Password == passwordtxt.Password);

            loginTbl.Login = logintxt.Text;
            loginTbl.Password = passwordtxt.Password;

            var LoginTbl = Database.GetInstance().LoginTbls.FirstOrDefault(s => s.RoleId == loginTbl.RoleId);

            if(loginTbl.Login == logintxt.Text && loginTbl.Password == passwordtxt.Password )
            {
                if(LoginTbl.RoleId == 1 )
                {
                    Menedsher menedsher = new Menedsher();
                    menedsher.Show();
                    Close();
                }
                if (LoginTbl.RoleId == 2)
                {
                    Pety menedsher = new Pety();
                    menedsher.Show();
                    Close();
                }
                else if(LoginTbl.RoleId != 1)
                {

                    MessageBox.Show("Вы не менеджер");
                }
                
                
            }
            else
            {
                MessageBox.Show("Логин или пароль не верный");
            }
        }
    }
}