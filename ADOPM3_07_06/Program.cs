using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Starting Main");
            var signal = new ManualResetEvent(false);

            new Thread(() =>
            {
                Console.WriteLine("4. Thread waiting for signal...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine("5. Thread got signal!");
            }).Start();

            Console.WriteLine("2. Main Sleeps for 2s");
            Thread.Sleep(2000);

            Console.WriteLine("3. Main sends signal to thread");
            signal.Set();        // “Open” the signal
        }
    }
    //Exercise
    //1.    Create 3 threads waiting for a signal. What happens?
}
