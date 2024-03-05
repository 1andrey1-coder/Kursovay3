using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Kursovay2.mvvm;
using Kursovay2.Views;

namespace Kursovay2.mvvm.VM
{
    public class MainVM
    {
        public PageControl PageControl { get; set; }

        public MainVM()
        {
            PageControl = PageControl.GetInstance();
            PageControl.CurrentPage = new Login();
            Application.Current.Exit += PageControl.OnAppClose;
        }
    }
}
