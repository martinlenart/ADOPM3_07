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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;

namespace ADOPM3_07_10
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
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            myCounter.Content = (int.Parse((string)myCounter.Content) + 1).ToString();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //async
            myGreetings.Text = "";

            var t1 =  await DownloadWebUrlAsync("https://dotnet.microsoft.com/");
            var t2 =  await DownloadWebUrlAsync("https://www.cnn.com/");
          
            myGreetings.Text = $"Async read nr char: {t1 + t2}";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Sync
            myGreetings.Text = "";

            var i1 = DownloadWebUrl("https://dotnet.microsoft.com/");
            var i2 = DownloadWebUrl("https://www.cnn.com/");

            myGreetings.Text = $"Sync read nr char: {i1 + i2}";
        }


        private static Task<int> DownloadWebUrlAsync(string url) => Task.Run(() => DownloadWebUrl(url));
        private static int DownloadWebUrl(string url)
        {
            using (var w = new System.Net.WebClient())
            {
                string page = null;
                Console.WriteLine($"Downloading {url as string}");
                for (int i = 0; i < 10; i++)
                {
                    page += w.DownloadString(url as string);
                }
                Console.WriteLine($"Downloaded {url as string}, length {page.Length}");
                return page.Length;
            }
        }
    }
}
