using System;
using System.Net;
using System.Threading;

namespace ADOPM3_07_04
{
    class Program
    {
        static void Main(string[] args)
        {
            int UnsafeResult = 0;

            object _locker = new object();
            int SafeResult = 0;

            new Thread(() =>
            {
                UnsafeResult = 12345;

                lock (_locker)
                {
                    SafeResult = 6789;
                }
            }).Start();

            Console.WriteLine($"UnsafeData: {UnsafeResult}");
            lock (_locker)
            {
                Console.WriteLine($"SafeResult: {SafeResult}");
            }
        }
    }
}
