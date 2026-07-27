using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Mary.Modules.Windows;

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

            MusicFolderSelect.LoadUserFolder(TvMusicFolder);
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem item)
            {
                MusicFolderSelect.PopulateSubFolders(item);
                e.Handled = true;
            }
        }

        private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem item)
            {
                MusicFolderSelect.CollapseFolder(item);
                e.Handled = true;
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (TvMusicFolder.SelectedItem is TreeViewItem selectedItem && selectedItem.Tag is string selectedPath)
            {
            }

            this.NavigationService?.Navigate(new wel_3());
        }
    }
}