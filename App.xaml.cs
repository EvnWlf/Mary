using System.Configuration;
using System.Data;
using System.Windows;
using System.IO;
using Mary.Windows;

namespace Mary
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool firstLogon = File.Exists("User.json");

            if (firstLogon)
            {
                var main = new Mary.Windows.MainWindow.Mary();
                main.Show();
            }
            else
            {
                var welcome = new Mary.Windows.WelcomeWindow.Welcome();
                welcome.Show();
            }

        }
    }

}
