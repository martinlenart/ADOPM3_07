using System.Diagnostics;
using System.Net;

namespace ADOPM3_07_01b;

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

        Console.WriteLine("\nAsync calls");
        timer = new Stopwatch();
        timer.Start();

        var t1 = MyThreadEntryPointAsync("https://www.cnn.com/");
        var t2 = MyThreadEntryPointAsync("https://www.bbc.com/");
        var t3 = MyThreadEntryPointAsync("https://dotnet.microsoft.com/");

        await Task.WhenAll(t1, t2, t3);
        Console.WriteLine("All Tasks finished");

        timer.Stop();
        Console.WriteLine($"{timer.ElapsedMilliseconds:N0}");

        Console.WriteLine("Main is finished");
        Console.ReadKey();


    }

    private static Task MyThreadEntryPointAsync(object arg) => Task.Run(() => MyThreadEntryPoint(arg));

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

