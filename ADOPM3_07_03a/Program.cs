using System.Net;
using static System.Net.WebRequestMethods;

namespace ADOPM3_07_03a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Data is not Thread safe, for illustration of variable capturing only
            int nrDownLoads = 10;        //Captured in LE - used as Thread input value and copied to local in the LE
            string url1 = "https://www.cnn.com/";

            var t1 = Task.Run(() =>
            {
                int localNrDownLoads = nrDownLoads;     //Copy the captured to a local variable
                int nrCharDownloaded = 0;
                using (var w = new WebClient())
                {
                    string page = null;
                    Console.WriteLine($"Downloading {url1 as string}");
                    for (int i = 0; i < localNrDownLoads; i++)
                    {
                        Console.WriteLine($"...t1 round:{i + 1}");
                        page += w.DownloadString(url1 as string);
                    }
                    Console.WriteLine($"t1 Downloaded {url1 as string}, length {page.Length}");
                    nrCharDownloaded += page.Length;
                }

                return nrCharDownloaded;
            });

            string url2 = "https://dotnet.microsoft.com/";
            var t2 = Task.Run(() =>
            {
                int localNrDownLoads = nrDownLoads;     //Copy the captured to a local variable
                int nrCharDownloaded = 0;
                using (var w = new WebClient())
                {
                    string page = null;
                    Console.WriteLine($"Downloading {url2 as string}");
                    for (int i = 0; i < localNrDownLoads; i++)
                    {
                        Console.WriteLine($"...t2 round:{i + 1}");
                        page += w.DownloadString(url2 as string);
                    }
                    Console.WriteLine($"t2 Downloaded {url2 as string}, length {page.Length}");
                    nrCharDownloaded += page.Length;
                }

                return nrCharDownloaded;
            });

            int nrCharDownloaded = t1.Result;
            nrCharDownloaded += t2.Result;

            Console.WriteLine($"Totalnr of character downloaded: {nrCharDownloaded}");
        }
    }
}