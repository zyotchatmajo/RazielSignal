using System;
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
using System.Windows.Shapes;

namespace RazielSignal
{
    /// <summary>
    /// Interaction logic for New_File.xaml
    /// </summary>
    public partial class New_File : Window
    {
        SocketClient _client;
        public New_File(SocketClient client)
        {
            InitializeComponent();
            _client = client;
        }

        private void Add_File_Click(object sender, RoutedEventArgs e) {
            string data;
            if (Add_File.Content.Equals("Add Folder")) {
                data = "Folder,Create," + TXTName.Text + "," + this.Path.Content;
                _client.fnSendInfoAsync(data);
            } else {
                data = "File,Create," + TXTName.Text + ".txt," + this.Path.Content;
                _client.fnSendInfoAsync(data);
            }
            
            this.Close();
        }
    }
}
