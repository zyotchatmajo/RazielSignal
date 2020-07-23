using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListAllFiles();
        }

        public void ListAllFiles()
        {
            FolderList.AddRange(Folder.ListAllFolders(Path));
            FolderList.AddRange(Text.ListAllTextFiles(Path));
            GridFolder.ItemsSource = FolderList;
        }

        private void CreateAFolder(object sender, RoutedEventArgs e)
        {
            New_File NewFileWindow = new New_File();
            NewFileWindow.Path.Content = Path;
            NewFileWindow.Add_File.Content = "Add Folder";
            NewFileWindow.Show();
            
        }
    }
}
