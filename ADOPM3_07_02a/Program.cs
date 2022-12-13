using System.Net;

namespace ADOPM3_07_02a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = Task.Run(() => MyThreadEntryPoint("https://www.cnn.com/"));
            Console.WriteLine($"t1 status: {t1.Status}");
            t1.Wait();

            var t2 = Task.Run(() => MyThreadEntryPoint("https://www.bbc.com/"));
            Console.WriteLine($"t1 status: {t1.Status}");
            Console.WriteLine($"t2 status: {t2.Status}");
            t2.Wait();

            var t3 = Task.Run(() => MyThreadEntryPoint("https://dotnet.microsoft.com/"));
            Console.WriteLine($"t1 status: {t1.Status}");
            Console.WriteLine($"t2 status: {t2.Status}");
            Console.WriteLine($"t3 status: {t3.Status}");
            t3.Wait();

            //Location of Join matters...
            //Task.WaitAll(t1, t2, t3);
            //Console.WriteLine("All Tasks finished");

            Console.WriteLine("Main is finished");
            Console.WriteLine($"t1 status: {t1.Status}");
            Console.WriteLine($"t2 status: {t2.Status}");
            Console.WriteLine($"t3 status: {t3.Status}");
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