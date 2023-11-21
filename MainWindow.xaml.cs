﻿using BotetteUI.Helper;
using BotetteUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Timers;
using System.Threading.Tasks;

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
            InitializeFiles();
            data = Data.Read(App_Helper.DataFilePath);
            config = Config.Read(App_Helper.ConfigFilePath);
            settings = Settings.Read(App_Helper.SettingsFilePath);
            InitializeUI();
            IsPBORunning();
        }

        public void InitializeFiles()
        {
            if (!App_Helper.SettingsFileExists)
            {
                MessageBox.Show("Please show the path to botette", "Start", MessageBoxButton.OK);
                Com_Helper.OpenDirectoryExplorer((path) =>
                {
                    Settings.Write(new Settings(path), App_Helper.SettingsFilePath);
                });
            }

            Settings settings = Settings.Read(App_Helper.SettingsFilePath);
            if (!App_Helper.DataFileExists) Data.Write(new Data(), App_Helper.DataFilePath);
            if (!App_Helper.ConfigFileExists) Config.Write(new Config(settings.WorkingDirectory), App_Helper.ConfigFilePath);
        }

        public void InitializeUI()
        {
            /* Set Checkboxes */
            cb_catch.IsChecked = config.Hunt;
            cb_run.IsChecked = config.RunFromFights;
            cb_debug.IsChecked = config.Debug;

            /* Set Textboxes */
            txt_working_directory.Text = settings.WorkingDirectory;
            txt_pbo_path.Text = settings.PBOPath;

            /* Set data */
            data.Pokeballs.ForEach(ball => cbx_ball.Items.Add(ball.Name));
            cbx_mons.ItemsSource = data.Pokemon;

            cbx_moves.SelectedItem = config.Move;
            data.Moves.ForEach(move => cbx_moves.Items.Add(move));

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
            if (cbx_mons.SelectedItem != null)
            {
                string selectedPkm = cbx_mons.SelectedItem.ToString()!;
                Pokeball selectedBall =
                    cbx_ball.SelectedItem == null ?
                    data.Pokeballs[(int)POKEBALLS.ULTRA_BALL] :
                    data.Pokeballs.FirstOrDefault(ball => ball.Name == cbx_ball.SelectedItem.ToString());

                Target target = new Target(selectedPkm, selectedBall);
                lv_hunts.Items.Add(target);
                config.Targets.Add(target);
                config = Config.Write(config, App_Helper.ConfigFilePath);
            }
            else Com_Helper.Error("No pokemon selected");

        }

        private void bttn_delete_selected_Click(object sender, RoutedEventArgs e)
        {
            if (lv_hunts.SelectedItem != null)
            {
                Target selectedItem = (Target)lv_hunts.SelectedItem;
                // config.Targets.Remove(selectedItem); BIG ?
                config.Targets.RemoveAll(target => target.Name == selectedItem.Name);
                lv_hunts.Items.Remove(selectedItem);
                config = Config.Write(config, App_Helper.ConfigFilePath);
            }
            else Com_Helper.Error("No pokemon selected");
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

        private void txt_pbo_path_Click(object sender, RoutedEventArgs e)
        {
            Com_Helper.OpenFileExplorer("Open File","",(path) =>
            {
                txt_pbo_path.Text = path;
                settings.PBOPath = path;
                Settings.Write(settings, App_Helper.SettingsFilePath);
            });
        }

        private void IsPBORunning()
        {
            Timer processCheckTimer = new Timer();
            processCheckTimer.Interval = 1000; // Check every 1 second (adjust as needed)
            processCheckTimer.Elapsed += ProcessCheckTimerElapsed!;
            processCheckTimer.Start();
        }

        private bool IsProcessRunning(string processName)
        {
            // Check if the specified process is running
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        private void ProcessCheckTimerElapsed(object sender, ElapsedEventArgs e)
        {
            bool isJavaRunning = IsProcessRunning("java");
            Dispatcher.Invoke(() =>
            {
                txt_game_running.Text = isJavaRunning ? "Game found" : "Game not running";
                txt_game_running.Foreground = isJavaRunning ? Brushes.Green : Brushes.Red;
                bttn_game_start.Visibility = isJavaRunning ? Visibility.Hidden : Visibility.Visible;
                bttn_start.Visibility = isJavaRunning ? Visibility.Visible : Visibility.Hidden;
            });
        }

        private void bttn_start_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                RunProcess("python.exe", settings.WorkingDirectory + "/main.py", true);
            });
        }

        private void bttn_game_start_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                RunProcess(settings.PBOPath);
            });
        }

        private void RunProcess(string filename, string args = "", bool useShell = true)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = filename;
                    process.StartInfo.Arguments = args;
                    process.StartInfo.UseShellExecute = useShell;
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
