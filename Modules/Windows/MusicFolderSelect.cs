using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mary.Modules.Windows
{
    public class MusicFolderSelect
    {
        public static void LoadUserFolder(TreeView treeView)
        {
            treeView.Items.Clear();
            try
            {
                string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                DirectoryInfo userDir = new DirectoryInfo(userProfilePath);

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

        public static TreeViewItem CreateFolderNode(string name, string path, bool isExpanded = false)
        {
            TreeViewItem item = new TreeViewItem { Tag = path };
            StackPanel stack = new StackPanel { Orientation = Orientation.Horizontal };

            FontFamily symbolFont = (Application.Current?.Resources?["SymbolThemeFontFamily"] as FontFamily)
                                    ?? new FontFamily("Segoe Fluent Icons");

            TextBlock icon = new TextBlock
            {
                FontFamily = symbolFont,
                Text = isExpanded ? "\xE838;" : "\xE8B7;",
                FontSize = 14,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 6, 0),
                Foreground = (Application.Current?.Resources?["TextFillColorSecondaryBrush"] as Brush)
                             ?? Brushes.Gray,
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

        public static void PopulateSubFolders(TreeViewItem item)
        {
            if (item.Items.Count == 1 && item.Items[0] as string == "*")
            {
                item.Items.Clear();
                string? dirPath = item.Tag as string;
                if (dirPath == null) return;

                try
                {
                    DirectoryInfo dir = new DirectoryInfo(dirPath);
                    foreach (DirectoryInfo subDir in dir.GetDirectories())
                    {
                        bool isSystemOrHidden = (subDir.Attributes & FileAttributes.Hidden) != 0 ||
                                                (subDir.Attributes & FileAttributes.System) != 0;
                        bool startsWithDot = subDir.Name.StartsWith(".");

                        if (!isSystemOrHidden && !startsWithDot)
                        {
                            TreeViewItem subItem = CreateFolderNode(subDir.Name, subDir.FullName, false);
                            item.Items.Add(subItem);
                        }
                    }
                }
                catch (UnauthorizedAccessException) { }
                catch (Exception) { }
            }
            UpdateFolderIcon(item, "\xE838;");
        }

        public static void CollapseFolder(TreeViewItem item)
        {
            UpdateFolderIcon(item, "\xE8B7;");
        }

        private static void UpdateFolderIcon(TreeViewItem item, string glyph)
        {
            if (item.Header is StackPanel stack)
            {
                foreach (var child in stack.Children)
                {
                    if (child is TextBlock tb && tb.Tag is string tag && tag == "FolderGlyph")
                    {
                        tb.Text = glyph;
                        break;
                    }
                }
            }
        }
        public static void SaveSelection(string selectedPath)
        {
            if (!string.IsNullOrWhiteSpace(selectedPath))
            {
                // Guardar la ruta en la configuración de la aplicación
                AppConfigManager.SaveMusicPath(selectedPath);

                // Indexar y guardar caché de pistas encontradas
                try
                {
                    var entries = IndexMusicFiles(selectedPath);
                    SaveMusicCache(entries);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al indexar música: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Busca recursivamente archivos de música en la carpeta seleccionada.
        /// Devuelve una lista simple de objetos con información básica para la caché.
        /// </summary>
        public static System.Collections.Generic.List<object> IndexMusicFiles(string rootPath)
        {
            var list = new System.Collections.Generic.List<object>();
            if (string.IsNullOrWhiteSpace(rootPath) || !Directory.Exists(rootPath))
                return list;

            string[] exts = new[] { ".mp3", ".flac", ".wav", ".m4a", ".aac", ".ogg" };

            try
            {
                foreach (var file in Directory.EnumerateFiles(rootPath, "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        string ext = Path.GetExtension(file)?.ToLowerInvariant() ?? string.Empty;
                        if (Array.IndexOf(exts, ext) >= 0)
                        {
                            var fi = new FileInfo(file);
                            list.Add(new
                            {
                                Path = file,
                                Name = fi.Name,
                                Length = fi.Length,
                                LastWrite = fi.LastWriteTimeUtc
                            });
                        }
                    }
                    catch { }
                }
            }
            catch { }

            return list;
        }

        /// <summary>
        /// Guarda la lista indexada en el directorio de configuración de la aplicación.
        /// </summary>
        private static void SaveMusicCache(System.Collections.Generic.List<object> entries)
        {
            try
            {
                var configDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MaryApp");
                if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);
                var cachePath = Path.Combine(configDir, "music_cache.json");
                var json = System.Text.Json.JsonSerializer.Serialize(entries, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(cachePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error guardando cache de música: " + ex.Message);
            }
        }
    }
}