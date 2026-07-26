using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Mary.Windows.WelcomeWindow
{
    public partial class wel_1 : Page
    {
        private readonly string _fullText = "Mary es una experiencia integral de música y lectura, concebida para ser una extensión natural de tu entorno digital.";

        public wel_1()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await TypeWriteEffectAsync(_fullText);

            BtnNext.BeginAnimation(UIElement.OpacityProperty,
                new System.Windows.Media.Animation.DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(400)));
        }

        private async Task TypeWriteEffectAsync(string text)
        {
            TxtParagraph.Text = "";
            foreach (char c in text)
            {
                TxtParagraph.Text += c; 
                await Task.Delay(28); 
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new wel_2());
        }
    }
}