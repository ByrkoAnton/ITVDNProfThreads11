using System;
using System.Threading;

namespace strreamsAdditional
{
    class Program
    {
        static readonly object Locker = new object();
        static int couter = 1;
        static void Main(string[] args)
        {
            
            Thread thread1 = new Thread(Counter);
            thread1.Name = "thread1";
            thread1.Start();

            Thread thread2 = new Thread(Counter);
            thread2.Name = "thread2";
            thread2.Start();

            Thread thread3 = new Thread(Counter);
            thread3.Name = "thread3";
            thread3.Start();

        }
        public static void Counter()
        {
            lock (Locker)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.Write($"{Thread.CurrentThread.Name} --- {couter++}");
                    Console.WriteLine();
                }
            }
        }
    }
}
