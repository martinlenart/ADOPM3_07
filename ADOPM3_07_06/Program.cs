using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var signal = new ManualResetEvent(false);

            new Thread(() =>
            {
                Console.WriteLine("Waiting for signal...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine("Got signal!");
            }).Start();

            Thread.Sleep(2000);
            signal.Set();        // “Open” the signal
        }
    }
    //Exercise
    //1.    Create 3 threads waiting for a signal. What happens?
}
