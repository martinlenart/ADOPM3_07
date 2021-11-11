﻿using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_03
{
    public static class myExtensions
    {
        public static ThreadState UsefulStates(this ThreadState ts)
        {
            return ts & (ThreadState.Unstarted | ThreadState.Running | ThreadState.WaitSleepJoin | ThreadState.Stopped);
        }
    }
    class Program
    {
        private static void Main(string[] args)
        {
            int nrDownLoads = 0;
            int nrCharDownloaded = 0;
            var t1 = new Thread((object url) =>
            {
                int localNrDownLoads = nrDownLoads;
                using (var w = new WebClient())
                {
                    string page = null;
                    Console.WriteLine($"Downloading {url as string}");
                    for (int i = 0; i < localNrDownLoads; i++)
                    {
                        Console.Write($"...t1:{i+1}");
                        page += w.DownloadString(url as string);
                    }
                    Console.WriteLine($"Downloaded {url as string}, length {page.Length}");
                    nrCharDownloaded += page.Length;
                }
            });
            var t2 = new Thread((object url) =>
            {
                int localNrDownLoads = nrDownLoads;
                using (var w = new WebClient())
                {
                    string page = null;
                    Console.WriteLine($"Downloading {url as string}");
                    for (int i = 0; i < localNrDownLoads; i++)
                    {
                        Console.Write($"...t2:{i+1}");
                        page += w.DownloadString(url as string);
                    }
                    Console.WriteLine($"Downloaded {url as string}, length {page.Length}");
                    nrCharDownloaded += page.Length;
                }
            });

            nrDownLoads = 2;
            t1.Start("https://www.cnn.com/");

            nrDownLoads = 3;
            t2.Start("https://dotnet.microsoft.com/");

            t1.Join();
            t2.Join();
            Console.WriteLine($"Totalnr of character downloaded: {nrCharDownloaded}");
        }
    }
    //Exercise
    //1.    Identify and explain the difference scoped variables, captured variable and passing data as thread argument.
    //2.    Modify thread codes to use nrDownloads directly and put the thread to sleep before using the variable, 
    //      meanwhile modifying for second thread. What happens?
    //2.    Create your own LE task using scoped, captured variables as well as passing data as thread argument.
    //      Do you get data syncronization problems?
}