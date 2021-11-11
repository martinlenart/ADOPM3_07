using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_02
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
            var t1 = new Thread(MyThreadEntryPoint);
            var t2 = new Thread(MyThreadEntryPoint);
            var t3 = new Thread(MyThreadEntryPoint);

            t1.Start("https://www.cnn.com/");
            Console.WriteLine(t1.ThreadState.UsefulStates());
            t1.Join();
            Console.WriteLine(t1.ThreadState.UsefulStates());

            t2.Start("https://www.bbc.com/");
            t2.Join();
            t3.Start("https://dotnet.microsoft.com/");
            t3.Join();

            Console.WriteLine("Sleep 5 seconds");
            Thread.Sleep(5000);
            Console.WriteLine("Done Sleeping!");
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
    //1.    Experiment by moving or removing some of the Join() and Sleep(). Discuss in the group what happens?
}
