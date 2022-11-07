using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InfoCard.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var model = new ApplicationViewModel();

            var t = model.LoadInfoCardsAsync();

            var mainWindow = new MainWindow(model);

            mainWindow.Show();
        }
    }
}
