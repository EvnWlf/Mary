using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Mary.Windows.SettingsWindow;

namespace Mary.Windows.MainWindow
{
    public partial class Mary : Window
    {
        public Mary()
        {
            InitializeComponent();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            global::Mary.Windows.SettingsWindow.Settings settingsWindow =
                new global::Mary.Windows.SettingsWindow.Settings();
            settingsWindow.Show();
        }
    }
}
