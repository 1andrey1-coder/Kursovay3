using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Kursovay2.mvvm.VM
{
    public class PageControl : BaseVM
    {
        static PageControl instance;

        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage != null)
                    OnAppClose(this, null);
                currentPage = value;
                Signal();
            }
        }

        internal static PageControl GetInstance()
        {
            if (instance == null)
                instance = new PageControl();
            return instance;
        }

        internal void OnAppClose(object sender, ExitEventArgs e)
        {
            ((BaseVM)CurrentPage.DataContext).OnClose();
        }
    }
}
