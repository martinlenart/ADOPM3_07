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
            using var signal = new ManualResetEvent(false);

            Console.WriteLine("Starting Thread");
            var t1 = new Thread(() =>
            {
                Console.WriteLine("Thread waiting for signal...");
                signal.WaitOne();
                Console.WriteLine("Thread got signal!");
            });
            t1.Start();

            Console.WriteLine("Main waits for user input");
            Console.ReadKey();

            Console.WriteLine("\nMain sends signal to thread");
            signal.Set();        // “Open” the signal

            t1.Join();         
        }
    }
    //Exercise
    //1.    Create 3 threads waiting for a signal. What happens?
}
