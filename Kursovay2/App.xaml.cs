using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kursovay2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            EventManager.RegisterClassHandler(typeof(Window), Window.LoadedEvent, 
                new RoutedEventHandler(WindowLoadedHandler));
        }

        private void WindowLoadedHandler(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


    }
}
