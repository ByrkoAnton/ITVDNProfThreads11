using System;
using System.IO;
using System.Text;
using System.Threading;

namespace strreams11
{
    class Program
    {
        static readonly object Locker = new object();

        static void Main(string[] args)
        {
            Writer writer1 = new Writer 
                {src = @"D:\test\poem1.txt", dst = @"D:\test\poem1,2.txt"};
            Thread myThread1 = new Thread(WriteInFile);
            myThread1.Start(writer1);

            Writer writer2 = new Writer 
                {src = @"D:\test\poem2.txt", dst = @"D:\test\poem1,2.txt"};
            Thread myThread2 = new Thread(WriteInFile);
            myThread2.Start(writer2);
        }


        public static void WriteInFile(object w)
        {
            Writer writer = (Writer) w; 
            lock (Locker)
            {
                var fileSrc = new FileInfo(writer.src);
                FileStream streamRead = fileSrc.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                byte[] array = new byte[streamRead.Length];
                streamRead.Read(array, 0, array.Length);
                streamRead.Close();

                var fileDst = new FileInfo(writer.dst);
                FileStream streamWrite = fileDst.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                streamWrite.Write(array);
                streamWrite.Close();
            }
        }
    }
   class Writer
    {
        public string src;
        public string dst;
    }
}
