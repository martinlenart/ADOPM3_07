using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ADOPM3_07_08
{
    class Program
    {
        private static void Main(string[] args)
        {
            var t1 = Task.Run(() => DownloadWebUrl("https://www.cnn.com/"));
            var t2 = Task.Run(() => DownloadWebUrl("https://dotnet.microsoft.com/"));

            Console.WriteLine(t1.IsCompleted);  // False
            Console.WriteLine(t2.IsCompleted);  // False
            t1.Wait();
            t2.Wait();

            //Write the result
            Console.WriteLine($"Totalnr of character downloaded: {t1.Result + t2.Result}");
            Console.WriteLine(t1.IsCompleted);  // True
            Console.WriteLine(t2.IsCompleted);  // True
        }
        private static int DownloadWebUrl(string url)
        {
            using (var w = new WebClient())
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
    //Exercise
    //1.    Remove the t1.Wait and t2.Wait and run. What happens? Why?
    //1.    Modify code to in a simgle statement wait for all tasks to complete
    //2.    Use a timer and set the timer right when start the tasks and stop when all tasks finished.
    //      Discuss in the group the difference when waiting on each task to finish and when waiting for all to fininsh. Explain.
}
