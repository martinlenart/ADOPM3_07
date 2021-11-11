using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_01
{
    class Program
    {
        private static void Main(string[] args)
        {
            var t1 = new Thread(MyThreadEntryPoint);
            var t2 = new Thread(MyThreadEntryPoint);
            var t3 = new Thread(MyThreadEntryPoint);

            t1.Start("https://www.cnn.com/");
            t2.Start("https://www.bbc.com/");
            t3.Start("https://dotnet.microsoft.com/");
        }

        private static void MyThreadEntryPoint(object arg)
        {
            string url = (string)arg;

            using (var w = new WebClient())
            {
                Console.WriteLine($"Downloading {url}");
                string page = w.DownloadString(url);
                Console.WriteLine($"Downloaded {url}, length {page.Length}");
            }
        }
    }
    //Exercise
    //1. Explore with the debugger the content of the downloaded page. 
    //2. Create a Task using Lambda Expression as the delegate
}
