﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace PhotoEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SettingsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SettingsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Settings ssettings = new Settings();
                ssettings.Show();
            }
            catch (Exception ex)
            {

            }
        }

        private void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            try
            {
                string filepath = tvPictures.SelectedValuePath;
                ImageSource imageSource = new BitmapImage(new Uri(filepath));
                imViewer.Source = imageSource;
            }
            catch (Exception ex)
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpDateTaken.SelectedDate = DateTime.Today;

                string defaultPath = Properties.Settings.Default.RetrieveFolder.ToString();
                ListDirectory(tvPictures, defaultPath);
            }
            catch (Exception ex)
            {

            }
        }

        private static void ListDirectory(TreeView treeView, string path)
        {
            treeView.Items.Clear();

            foreach (string s in Directory.GetFiles(path))
            {
                TreeViewItem subitem = new TreeViewItem();
                subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                subitem.Tag = s;
                subitem.FontWeight = FontWeights.Normal;
                treeView.Items.Add(subitem);
            }
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Settings = new RoutedUICommand
            (
                "Settings",
                "Settings",
                typeof(CustomCommands)
            );
    }
}