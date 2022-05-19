using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace ADOPM3_07_09
{
    class Program
    {
        private static void Main(string[] args)
        {
            var t1 = DownloadWebUrlAsync("https://www.cnn.com/");
            var t2 = DownloadWebUrlAsync("https://dotnet.microsoft.com/");
            
            try
            {
                Console.WriteLine(t1.IsCompleted);  // False
                Console.WriteLine(t2.IsCompleted);  // False
                //t1.Wait();
                //t2.Wait();

                //Write the result
                Console.WriteLine($"Totalnr of character downloaded: {t1.Result + t2.Result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine(t1.IsFaulted); // true
                Console.WriteLine(t1.IsCompleted); // true
                Console.WriteLine(t1.IsCanceled); // false

                Console.WriteLine();
                Console.WriteLine(t2.IsFaulted); // false
                Console.WriteLine(t2.IsCompleted); // false
                Console.WriteLine(t2.IsCanceled); // false
            }
        }

        private static Task<int> DownloadWebUrlAsync(string url) => Task.Run(() => DownloadWebUrl(url));

        private static int DownloadWebUrl(string url)
        {
            using (var w = new WebClient())
            {
                string page = null;
                Console.WriteLine($"Downloading {url as string}");
                for (int i = 0; i < 10; i++)
                {
                    page += w.DownloadString(url as string);

                    //first task reaching 5 iterations throws an error
                    if (i == 5) throw new Exception("Error in task!");
                }
                Console.WriteLine($"Downloaded {url as string}, length {page.Length}");
                return page.Length;
            }
        }
    }
    //Exercise
    //1.    Remove the t1.Wait() and t2.Wait(). what hapens?
    //2.    In addition remove t1.Result and t2.Result. What happens? Explain what happens to the exception handling.
}
