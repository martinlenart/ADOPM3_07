using System;
using System.Net;
using System.Threading;


namespace ADOPM3_07_05
{
    class Program
    {
        //Create a class where lock is included in the get/set is a good 
        //way to minimize the risk of deadlocks
        //Threadsafe
        public class SafeData
        {
            object _locker = new object();
            int _safeData;

            public int Data
            {
                get
                {
                    lock (_locker) { return _safeData; }
                }
                set
                {
                    lock (_locker) { _safeData = value; }
                }
            }
        }
        static void Main(string[] args)
        {
            SafeData SafeData = new SafeData();

            new Thread(() =>
            {
                SafeData.Data = 123456789;

            }).Start();

            Console.WriteLine($"SafeData: {SafeData.Data}");
        }
    }
    //Exercise
    //1.    Discuss withing the group, how would you manage addition or substratcion on the Data? 
    //      Try it by starting a couple of threads making additions and subtraction. How do you manage data integrity?
    //2.    Add a method for addition and overload the + operator for a thread sfae addition.
    //3.    Create your own safe Data class for a Generic type T. Test it by starting a couple of threads accessing and modyfying the data
    //4.    Discuss in the group. What is thread safe? is your safe Data class thread safe? why?
}
