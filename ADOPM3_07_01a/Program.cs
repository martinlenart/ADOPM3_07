using System.Diagnostics;
using System.Net;

namespace ADOPM3_07_01a
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();

            Console.WriteLine("Syncron calls");
            MyThreadEntryPoint("https://www.cnn.com/");
            MyThreadEntryPoint("https://www.bbc.com/");
            MyThreadEntryPoint("https://dotnet.microsoft.com/");
            
            timer.Stop();
            Console.WriteLine($"{timer.ElapsedMilliseconds:N0}");

            Console.WriteLine("\nConcurrent execution");
            timer = new Stopwatch();
            timer.Start();
            
            var t1 = Task.Run(() => MyThreadEntryPoint("https://www.cnn.com/"));
            //t1.Wait();

            var t2 = Task.Run(() => MyThreadEntryPoint("https://www.bbc.com/"));
            //t2.Wait();

            var t3 = Task.Run(() => MyThreadEntryPoint("https://dotnet.microsoft.com/"));
            //t3.Wait();
            

            //Location of Join matters...
            Task.WaitAll(t1, t2, t3);
            Console.WriteLine("All Tasks finished");
            
            timer.Stop();
            Console.WriteLine($"{timer.ElapsedMilliseconds:N0}");

            Console.WriteLine("Main is finished");
            Console.ReadKey();         
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
}