using System.Net;

namespace ADOPM3_07_01a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = Task.Run(()=>MyThreadEntryPoint("https://www.cnn.com/"));
            var t2 = Task.Run(() => MyThreadEntryPoint("https://www.bbc.com/"));
            var t3 = Task.Run(() => MyThreadEntryPoint("https://dotnet.microsoft.com/"));

            //Location of Join matters...
            //Task.WaitAll(t1, t2, t3);
            //Console.WriteLine("All Tasks finished");

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