using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Controls;
namespace betterquest
{
    public partial class PreLoad : Page
    {
        public static bool IfDone = false;
        public static string FileSavePath;
        public static string IPAdressOfServer;
        public PreLoad()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileSavePath = "Error";
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                FileSavePath = dialog.FileName;

            }
            PathButton1.Content = FileSavePath;
            if (FileSavePath != "Error")
            {
                TextNenych.Visibility = Visibility.Hidden;
                ReadyToGo.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IfDone = true;
            IPAdressOfServer = IPAdressOfServer1.Text;
            if (PreLoad.IfDone == true)
            {
               MainWindow.DownloadGamesList();
            }
            if (MainWindow.temp != 0)
            {
                NavigationService.Navigate(new Games());
            }
        }
    }
}
