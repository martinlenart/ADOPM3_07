using System;
using System.Net;
using System.Threading;


namespace ADOPM3_07_05
{
    class Program
    {
        public class SafeData
        {
            object _locker = new object();
            int iSafeResult1 = 0;
            int iSafeResult2 = 0;

            public void SetData(int sf1, int sf2)
            {
                lock (_locker)
                {
                    iSafeResult1 = sf1;
                    iSafeResult2 = sf2;
                }
            }
            public (int, int) GetData()
            {
                lock (_locker) { return (iSafeResult1, iSafeResult2); }
            }
        }

        static void Main(string[] args)
        {
            var mySafe = new SafeData();

            new Thread(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    mySafe.SetData(8888, 8888);
                    (int i1, int i2) = mySafe.GetData();
                    if (i1 != i2)
                        Console.WriteLine("mySafe mismatch");
                }
            }).Start();

            new Thread(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 100; i++)
                {
                    mySafe.SetData(9999, 9999);
                    (int i1, int i2) = mySafe.GetData();
                    if (i1 != i2)
                        Console.WriteLine("mySafe mismatch");
                }
            }).Start();
        }
    }    //Exercise
    //1.    Discuss withing the group, how would you manage addition or substratcion on the Data? 
    //      Try it by starting a couple of threads making additions and subtraction. How do you manage data integrity?
    //2.    Add a method for addition and overload the + operator for a thread sfae addition.
    //3.    Create your own safe Data class for a Generic type T. Test it by starting a couple of threads accessing and modyfying the data
    //4.    Discuss in the group. What is thread safe? is your safe Data class thread safe? why?
}
