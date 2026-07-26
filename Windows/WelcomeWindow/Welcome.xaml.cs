using System.Windows;
using System.Windows.Input;

namespace Mary.Windows.WelcomeWindow
{
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();
            MainFrame.Navigate(new wel_1());
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}