using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Mary.Windows.WelcomeWindow
{
    public partial class wel_2 : Page
    {
        public wel_2()
        {
            InitializeComponent();
            this.Loaded += Wel_2_Loaded;
        }

        private void Wel_2_Loaded(object sender, RoutedEventArgs e)
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
            this.NavigationService?.Navigate(new wel_3());
        }
    }
}