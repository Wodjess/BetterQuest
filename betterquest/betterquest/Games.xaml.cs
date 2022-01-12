using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
namespace betterquest
{
    public partial class Games : Page
    {
        public string[][] GamesArray;
        public Games()
        {
            InitializeComponent();
            GamesLoading();
        }
        void DownloadEvent(Object sender, EventArgs e)
        {
            string tag = ((sender as Button).Tag).ToString();
            string name = ((sender as Button).Name).ToString();
            try
            {
                Download(Fixed.FixFirstLetter(GamesArray[int.Parse(tag)][3]), PreLoad.FileSavePath, name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry something went wrong " + ex.ToString());
                MessageBox.Show(GamesArray[int.Parse(tag)][3]);
            }
        }
        public void Download(string url, string save_path, string name)
        {
            string fl = @"\";
             WebClient wc = new WebClient();
              int i = 0;
              wc.DownloadProgressChanged += (s, e) =>
              {
                  i += 1;
                  DownloadingTextBlock.Text = "Downloading: " + i;
              };
              wc.DownloadFileCompleted += (s, e) =>
              {
                  MessageBox.Show("Your file has been downloaded");
                  DownloadingTextBlock.Text = "";
              };
              wc.DownloadFileAsync(new Uri(url), save_path + fl + name);

        }
        public void GamesLoading()
        {
            GamesArray = MainWindow.GamesList;
            for (int i = 0; i < GamesArray.Length; i++)
            {
                Grid GameGrid = new Grid();
                GameGrid.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                GameGrid.Height = 100;
                GameGrid.Children.Add(
                    new TextBlock
                    {
                        Text = GamesArray[i][0],
                        Margin = new System.Windows.Thickness(157, 8, 3, 80),
                        FontFamily = new FontFamily("Arial Black")
                    }
                    );
                GameGrid.Children.Add(
                    new TextBlock
                    {
                        Text = GamesArray[i][1],
                        TextWrapping = System.Windows.TextWrapping.Wrap,
                        Margin = new System.Windows.Thickness(157, 22, 24, 38),
                        FontFamily = new FontFamily("Bahnschrift SemiBold Condensed"),
                        FontSize = 18
                    }
                    );
                Button button = new Button();
                button.Tag = i.ToString();
                try
                {
                    button.Name = GamesArray[i][0].ToString();
                }
                catch
                {
                    button.Name = "error";
                }
                button.Content = "Download";
                if (GamesArray[i][3] != null)
                    button.Background = (Brush)(new BrushConverter().ConvertFrom("#181735"));
                else
                    button.Background = (Brush)(new BrushConverter().ConvertFrom("#808080"));

                button.Foreground = Brushes.White;
                button.Margin = new System.Windows.Thickness(157, 65, 345, 10);
                button.Click += DownloadEvent;
                button.FontFamily = new FontFamily("Arial Black");
                GameGrid.Children.Add(button);

                //картинка с вапросикам)
                Image GameImage0 = new Image();
                GameImage0.Margin = new System.Windows.Thickness(10, 10, 436, 10);
                BlurEffect BlurEffect = new BlurEffect();
                BlurEffect.Radius = 5;
                GameImage0.Effect = BlurEffect;
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.DecodePixelWidth = 200;
                GameImage0.Source = myBitmapImage;
                if (GamesArray[i][3] == null)
                {
                    myBitmapImage.UriSource = new Uri("../NotEnoghtImage.png", UriKind.Relative);

                }
                else
                {
                    try
                    {
                        myBitmapImage.UriSource = new Uri(GamesArray[i][2], UriKind.Absolute);
                        BlurEffect.Radius = 0;
                    }
                    catch
                    {
                        myBitmapImage.UriSource = new Uri("../NotEnoghtImage.png", UriKind.Relative);
                    }
                }
                myBitmapImage.EndInit();
                GameGrid.Children.Add(GameImage0);
                //картинка с вапросикам)
                GameListScroller.Children.Add(GameGrid);
            }
        }
        void AdminDownloadServer(object sender, RoutedEventArgs e)
        {
            try
            {
                Download("../File/Server.rar", PreLoad.FileSavePath, "Server.rar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex);
            }
        }
    }
}
