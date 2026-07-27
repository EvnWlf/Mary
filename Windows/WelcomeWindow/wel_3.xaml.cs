using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Mary.Modules.Windows;

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

            BookFolderSelect.LoadUserFolder(BooksFolder);
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem item)
            {
                BookFolderSelect.PopulateSubFolders(item);
                e.Handled = true;
            }
        }

        private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TreeViewItem item)
            {
                BookFolderSelect.CollapseFolder(item);
                e.Handled = true;
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (BooksFolder.SelectedItem is TreeViewItem selectedItem && selectedItem.Tag is string selectedPath)
            {
            }

            global::Mary.Windows.MainWindow.Mary mary =
            new global::Mary.Windows.MainWindow.Mary();
            mary.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}