using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Main");
            var signal = new ManualResetEvent(false);

            Console.WriteLine("Starting Thread");
            new Thread(() =>
            {
                Console.WriteLine("Thread waiting for signal...");
                signal.WaitOne();
                Thread.Sleep(2000);
                signal.Dispose();
                Console.WriteLine("Thread got signal!");
            }).Start();

            Console.WriteLine("Main Sleeps for 2s");
            Thread.Sleep(2000);

            Console.WriteLine("Main sends signal to thread");
            signal.Set();        // “Open” the signal
        }
    }
    //Exercise
    //1.    Create 3 threads waiting for a signal. What happens?
}
