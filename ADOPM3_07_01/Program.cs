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

            t3.IsBackground = true;             //dies with main thread

            t1.Start("https://www.cnn.com/");
            t2.Start("https://www.bbc.com/");
            t3.Start("https://dotnet.microsoft.com/");

            Console.WriteLine("Main finished");
        }

        private static void MyThreadEntryPoint(object arg)
        {
            string url = (string)arg;

            using (var w = new WebClient())
            {
                Console.WriteLine($"Downloading {url}");
                try
                {
                    string page = w.DownloadString(url);
                    Console.WriteLine($"Downloaded {url}, length {page.Length}");
                }
                catch
                {
                    Console.WriteLine("Connection error");
                }
            }
        }
    }
    //Exercise
    //1. Explore with the debugger the content of the downloaded page. 
    //2. Create a Thread, t4, using Lambda Expression as the delegate to define the Thread activity. Don't forget to start the Thread.
}
