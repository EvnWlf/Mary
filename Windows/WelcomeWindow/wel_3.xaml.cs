using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mary.Windows.WelcomeWindow
{
    public partial class wel_3 : Page
    {
        public wel_3()
        {
            InitializeComponent();
            this.Loaded += Wel_3_Loaded;
        }

        private void Wel_3_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeIn = new DoubleAnimation(0.0, 1.0, TimeSpan.FromMilliseconds(700))
            {
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };
            this.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            TranslateTransform translate = new TranslateTransform(30, 0);
            this.RenderTransform = translate;

            DoubleAnimation slideIn = new DoubleAnimation(30.0, 0.0, TimeSpan.FromMilliseconds(700))
            {
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };
            translate.BeginAnimation(TranslateTransform.XProperty, slideIn);
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            Mary.Windows.MainWindow.Mary mainWindow = new Mary.Windows.MainWindow.Mary();
            mainWindow.Show();

            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }





    }
}
