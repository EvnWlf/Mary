using System;
using System.IO;
using System.Text.Json;

namespace Mary.Modules.Windows
{
    public class AppConfig
    {
        public string MusicFolderPath { get; set; } = string.Empty;
        public string BooksFolderPath { get; set; } = string.Empty;
    }

    public static class AppConfigManager
    {
        private static readonly string ConfigFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MaryApp");
        private static readonly string ConfigFilePath = Path.Combine(ConfigFolder, "config.json");

        public static AppConfig LoadConfig()
        {
            if (!File.Exists(ConfigFilePath))
                return new AppConfig();

            string json = File.ReadAllText(ConfigFilePath);
            return JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        }

        public static void SaveMusicPath(string path)
        {
            var config = LoadConfig();
            config.MusicFolderPath = path;
            SaveToFile(config);
        }

        public static void SaveBooksPath(string path)
        {
            var config = LoadConfig();
            config.BooksFolderPath = path;
            SaveToFile(config);
        }

        private static void SaveToFile(AppConfig config)
        {
            if (!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);

            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, json);
        }
    }
}