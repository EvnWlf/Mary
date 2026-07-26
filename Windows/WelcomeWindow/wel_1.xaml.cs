using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Mary.Windows.WelcomeWindow
{
    public partial class wel_1 : Page
    {
        private readonly string part1 = "Mary es una experiencia integral de música y lectura, concebida para ser una extensión natural de tu entorno digital. Aún nos encontramos en continuo desarrollo. Si disfrutas la experiencia, te invitamos a dejarnos una estrella en ";
        private readonly string part2 = "GitHub";
        private readonly string part3 = " para apoyar nuestro proyecto.";

        public wel_1()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await TypeWriterEffectAsync();
            AnimateTitleAndElements();
        }

        private async Task TypeWriterEffectAsync()
        {
            TxtParagraph.Inlines.Clear();

            Run run1 = new Run();
            TxtParagraph.Inlines.Add(run1);

            foreach (char c in part1)
            {
                run1.Text += c;
                await Task.Delay(22);
            }

            Hyperlink link = new Hyperlink(new Run(part2))
            {
                NavigateUri = new Uri("https://github.com"),
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111111")),
                FontWeight = FontWeights.SemiBold,
                TextDecorations = TextDecorations.Underline
            };
            link.RequestNavigate += Hyperlink_RequestNavigate;

            TxtParagraph.Inlines.Add(link);

            Run run3 = new Run();
            TxtParagraph.Inlines.Add(run3);

            foreach (char c in part3)
            {
                run3.Text += c;
                await Task.Delay(22);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void AnimateTitleAndElements()
        {
            DoubleAnimation scaleAnim = new DoubleAnimation(1.0, 2.1, TimeSpan.FromMilliseconds(850))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            DoubleAnimation fadeInNext = new DoubleAnimation(0.0, 0.5, TimeSpan.FromMilliseconds(700))
            {
                EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut }
            };

            TitleScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
            TitleScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);
            BtnNext.BeginAnimation(UIElement.OpacityProperty, fadeInNext);
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new wel_2());
        }
    }
}