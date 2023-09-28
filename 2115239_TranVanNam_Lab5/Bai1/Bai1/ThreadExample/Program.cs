using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExample
{
    class Program
    {
        static void Main(string[] args)
        {
            MyThreadClass mtc1 = new MyThreadClass("Day la tieu trinh thu 1");
            Thread thread1 = new Thread(new ThreadStart(mtc1.runMyThread));
            thread1.Start();

            MyThreadClass mtc2 = new MyThreadClass("Day la tieu trinh thu 2");
            Thread thread2 = new Thread(new ThreadStart(mtc1.runMyThread));
            thread2.Start();

            Console.ReadKey();

        }
    }
}
