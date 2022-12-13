namespace ADOPM3_07_05a
{
    internal class Program
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
            var SafeStorage = new SafeData();

            var t1 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 10_000; i++)
                {
                    SafeStorage.SetData(8888, 8888);
                    (int i1, int i2) = SafeStorage.GetData();
                    if (i1 != i2)
                        Console.WriteLine("mySafe mismatch");
                }
                Console.WriteLine("t1 Finished");
            });

            var t2 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 10_000; i++)
                {
                    SafeStorage.SetData(1111, 1111);
                    (int i1, int i2) = SafeStorage.GetData();
                    if (i1 != i2)
                        Console.WriteLine("mySafe mismatch");
                }
                Console.WriteLine("t2 Finished");
            });

            Task.WaitAll(t1, t2);
            Console.WriteLine("All Finished");
        }
    }    
    
    //Exercise
    //1.    Discuss withing the group, how would you manage addition or substratcion of iSafeResult1 and iSafeResult2? 
    //      Try it by starting a couple of Taks making additions and subtraction. How do you manage data integrity?
    //2.    Create your own safe Data class for a Generic type T. Test it by starting a couple of Tasks accessing and modyfying the data
    //3.    Discuss in the group. What is thread safe? is your safe Data class thread safe? why?
}