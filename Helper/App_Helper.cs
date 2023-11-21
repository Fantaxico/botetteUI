using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotetteUI.Models;

namespace BotetteUI.Helper
{
    public struct App_Helper
    {
        public static string SettingsFilePath = "./settings.json";
        public static string DataFilePath = "./data.json";
        public static string ConfigFilePath = relativeDir("/config.json");

        public static bool SettingsFileExists => File.Exists(SettingsFilePath);
        public static bool DataFileExists => File.Exists(DataFilePath);
        public static bool ConfigFileExists => File.Exists(ConfigFilePath);

        private static string relativeDir(string path)
        {
            if (SettingsFileExists)
            {
                Settings settings = Settings.Read(SettingsFilePath);
                return settings.WorkingDirectory + path;
            }
            return path;
        }
        public static bool IsNullOrEmpty<T>(T obj)
        {
            if (obj == null || obj is string && string.IsNullOrEmpty((string)(object)obj))
            {
                return true;
            }

            return false;
        }
    }
}
