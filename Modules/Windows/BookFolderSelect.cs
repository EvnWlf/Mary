using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Mary.Modules.Windows
{
    class BookFolderSelect
    {
        public static void LoadUserFolder(TreeView treeView)
        {
            treeView.Items.Clear();
            try
            {
                string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                DirectoryInfo userDir = new DirectoryInfo(userFolderPath);

                if (userDir.Exists)
                {
                    TreeViewItem rootItem = CreateFolderNode(userDir.Name, userDir.FullName, true);
                    treeView.Items.Add(rootItem);
                    rootItem.IsExpanded = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Creates a folder node with a glyph and a lazy-loading marker for subfolders.
        public static TreeViewItem CreateFolderNode(string name, string path, bool isExpanded = false)
        {
            TreeViewItem item = new TreeViewItem { Tag = path };
            StackPanel stack = new StackPanel { Orientation = Orientation.Horizontal };

            TextBlock icon = new TextBlock
            {
                Text = isExpanded ? "\xE838;" : "\xE8B7;",
                FontSize = 14,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 6, 0),
                Tag = "FolderGlyph"
            };

            TextBlock text = new TextBlock
            {
                Text = name,
                VerticalAlignment = VerticalAlignment.Center
            };

            stack.Children.Add(icon);
            stack.Children.Add(text);
            item.Header = stack;
            item.Items.Add("*");
            return item;
        }

        public static void PopulateSubFolders(TreeViewItem parentItem)
        {
            if (parentItem.Items.Count == 1 && parentItem.Items[0] as string == "*")
            {
                parentItem.Items.Clear();
                string? parentPath = parentItem.Tag as string;
                if (parentPath == null) return;

                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(parentPath);
                    foreach (var subDir in dirInfo.GetDirectories())
                    {
                        bool isSystemOrHidden = (subDir.Attributes & FileAttributes.Hidden) != 0 ||
                                                (subDir.Attributes & FileAttributes.System) != 0;
                        bool startsWithDot = subDir.Name.StartsWith(".");

                        if (!isSystemOrHidden && !startsWithDot)
                        {
                            TreeViewItem subItem = CreateFolderNode(subDir.Name, subDir.FullName, false);
                            parentItem.Items.Add(subItem);
                        }
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // Use the same glyph code used when creating the node to keep icons consistent
            UpdateFolderIcon(parentItem, "\xE838;");
        }

        public static void CollapseFolder(TreeViewItem item)
        {
            // Use the standard collapsed folder glyph
            UpdateFolderIcon(item, "\xE8B7;");
        }

        private static void UpdateFolderIcon(TreeViewItem item, string symbol)
        {
            if (item.Header is StackPanel stack)
            {
                foreach (var child in stack.Children)
                {
                    if (child is TextBlock tb && tb.Tag as string == "FolderGlyph")
                    {
                        tb.Text = symbol;
                        break;
                    }
                }
            }
        }
        public static void SaveSelection(string selectedPath)
        {
            if (!string.IsNullOrWhiteSpace(selectedPath))
            {
                AppConfigManager.SaveBooksPath(selectedPath);
            }
        }
    }
}