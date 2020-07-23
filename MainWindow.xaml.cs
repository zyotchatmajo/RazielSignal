﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace RazielSignal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<File> FolderList = new List<File>();
        public static string Path = @"c:\RazielSignal";
        SocketClient socket;

        public MainWindow()
        {
            InitializeComponent();
            socket = new SocketClient("192.168.100.13", 5000);
            socket.fnConnectSocket();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<File> test = SendReceive(Path);
            ListAllFiles(test);
        }

        public void ListAllFiles(List<File> msg)
        {
            GridFolder.ItemsSource = null;
            GridFolder.ItemsSource = msg;
        }

        public List<File> SendReceive(string value) {
            socket.fnSendInfo(value);
            return socket.fnReceiveInfo();
        }

        private void CreateAFolder(object sender, RoutedEventArgs e)
        {
            New_File NewFileWindow = new New_File();
            NewFileWindow.Path.Content = Path;
            NewFileWindow.Add_File.Content = "Add Folder";
            NewFileWindow.Show();
            
        }

        private void GridFolder_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            File file = (File)GridFolder.SelectedItem;
            List<File> value = SendReceive(file.Path);
            Path = file.Path;
            ListAllFiles(value);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            String[] newPath = Path.Split('\\');
            newPath[newPath.Length - 1] = "";
            Path = String.Join("\\",newPath);
            Path = Path.Substring(0, Path.Length - 1);
            List<File> value = SendReceive(Path);
            ListAllFiles(value);
        }
    }
}
