using BotetteUI.Helper;
using BotetteUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BotetteUI
{
    public partial class MainWindow : Window
    {
        Data data;
        Config config;
        Settings settings;

        public MainWindow()
        {
            InitializeComponent();
            InitApplication();
        }

        public void InitApplication()
        {
            if (!App_Helper.SettingsFileExists) {
                Com_Helper.OpenDirectoryExplorer((path) =>
                {
                    MessageBox.Show("Please show the path to botette", "Start", MessageBoxButton.OK);
                    Settings.Write(new Settings(path), App_Helper.SettingsFilePath);
                });
            }
            settings = Settings.Read(App_Helper.SettingsFilePath);
            if (!App_Helper.DataFileExists) Data.Write(new Data(), App_Helper.DataFilePath);
            if (!App_Helper.ConfigFileExists) Config.Write(new Config(settings.WorkingDirectory), App_Helper.ConfigFilePath);
            data = Data.Read(App_Helper.DataFilePath);
            config = Config.Read(App_Helper.ConfigFilePath);
            cb_catch.IsChecked = config.Hunt;
            cb_run.IsChecked = config.RunFromFights;
            cb_debug.IsChecked = config.Debug;
            txt_working_directory.Text = settings.WorkingDirectory;
            cbx_mons.ItemsSource = data.Pokemon;
            data.Moves.ForEach(move => cbx_moves.Items.Add(move));
            cbx_moves.SelectedItem = config.Move;
            config.Targets.ForEach(target => lv_hunts.Items.Add(target));
        }

        private void txt_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = txt_filter.Text.ToLower();
            var suggestions = data.Pokemon.Where(s => s.ToLower().Contains(filterText)).ToList();
            cbx_mons.ItemsSource = suggestions;
        }

        private void bttn_add_Click(object sender, RoutedEventArgs e)
        {
            RefreshPokemon(add: true, cbx_mons.SelectedItem);
        }

        private void bttn_delete_selected_Click(object sender, RoutedEventArgs e)
        {
            RefreshPokemon(add: false, lv_hunts.SelectedItem);
        }

        private void bttn_delete_all_Click(object sender, RoutedEventArgs e)
        {
            Com_Helper.YesNo("Really flush the list ?", "Caution", () =>
            {
                lv_hunts.Items.Clear();
                config.Targets.Clear();
                config = Config.Write(config, App_Helper.ConfigFilePath);
            });
        }

        private void cbx_moves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object? selectedItem = cbx_moves.SelectedItem;
            if (cbx_moves.SelectedItem == null) Com_Helper.Error("Non selected");
            else
            {
                string selectedMove = selectedItem.ToString()!;
                config.Move = selectedMove;
                config = Config.Write(config, App_Helper.ConfigFilePath);
            }
        }
        public void RefreshPokemon(bool add, object obj)
        {
            object? selectedObj = obj;
            if (selectedObj == null) Com_Helper.Error("Non selected");
            else
            {
                string selectedItem = selectedObj.ToString()!;
                if (add)
                {
                    lv_hunts.Items.Add(selectedItem);
                    config.Targets.Add(selectedItem);
                }
                else
                {
                    lv_hunts.Items.Remove(selectedItem);
                    config.Targets.Remove(selectedItem);
                }
                config = Config.Write(config, App_Helper.ConfigFilePath);
            }
        }

        private void cb_run_Change(object sender, RoutedEventArgs e)
        {
            config.RunFromFights = cb_run.IsChecked!.Value;
            config = Config.Write(config, App_Helper.ConfigFilePath);
        }

        private void cb_debug_Change(object sender, RoutedEventArgs e)
        {
            config.Debug = cb_debug.IsChecked!.Value;
            config = Config.Write(config, App_Helper.ConfigFilePath);
        }

        private void cb_catch_Change(object sender, RoutedEventArgs e)
        {
            config.Hunt = cb_catch.IsChecked!.Value;
            config = Config.Write(config, App_Helper.ConfigFilePath);
        }

        private void txt_working_directory_Click(object sender, RoutedEventArgs e)
        {
            Com_Helper.OpenDirectoryExplorer((path) =>
            {
                txt_working_directory.Text = path;
                settings.WorkingDirectory = path;
                Settings.Write(settings, App_Helper.SettingsFilePath);
            });
        }

        private void bttn_start_Click(object sender, RoutedEventArgs e)
        {
            RunPythonScript();
        }

        private void RunPythonScript()
        {
            string scriptPath = @"C:\Users\Luke\Documents\Dev\Projekte\botette\main.py";

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "python.exe"; // or the path to your Python interpreter
                process.StartInfo.Arguments = scriptPath;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.CreateNoWindow = false;

                // Start the process
                process.Start();

                // Read the output (if needed)
                //string output = process.StandardOutput.ReadToEnd();

                // Wait for the process to exit
                process.WaitForExit();

                // Optionally, do something with the output
                //MessageBox.Show(output);
            }
        }
    }
}
