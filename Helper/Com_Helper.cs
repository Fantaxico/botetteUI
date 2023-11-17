using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace BotetteUI.Helper
{
    public struct Com_Helper
    {
        public static void Error(string error, Action? action = null)
        {
            MessageBox.Show($"Error occurred:\n{error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            if (action != null) action();
        }

        public static void YesNo(string text, string header, Action? yesAction = null, Action? noAction = null)
        {
            var result = MessageBox.Show(text, header, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes && yesAction != null) yesAction();
            else if (result == MessageBoxResult.No && noAction != null) noAction();
        }

        public static string? SaveFileExplorer(string title = "Save File", string filter = "", Action<string>? action = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.Title = title;
            if (dialog.ShowDialog() == true)
            {
                string outputPath = dialog.FileName;
                if (action != null) action(outputPath);
                return outputPath;
            }
            return null;
        }

        public static string? OpenFileExplorer(string title = "Open File", string filter = "", Action<string>? action = null)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.Title = title;
            if (dialog.ShowDialog() == true)
            {
                string outputPath = dialog.FileName;
                if (action != null) action(outputPath);
                return outputPath;
            }
            return null;
        }

        public static string? OpenDirectoryExplorer(Action<string>? action = null)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string outputPath = dialog.SelectedPath;
                if (action != null) action(outputPath);
                return outputPath;
            }
            return null;
        }
    }
}
