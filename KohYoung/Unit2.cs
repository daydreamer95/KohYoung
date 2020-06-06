using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KohYoung
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    namespace KohYoung
    {
        public class Unit2
        {
            const int numberLimit = 10;
            const int numberCrash = 5;

            static object monitor = new object();

            public void Process()
            {
                Thread oddThread = new Thread(Odd);
                Thread evenThread = new Thread(Even);
                evenThread.Start();
                Thread.Sleep(1000);
                oddThread.Start();
                oddThread.Join();
                evenThread.Join();

                Console.WriteLine("The Program has been terminated!");
                Console.Read();
            }

            public static void Odd()
            {
                try
                {
                    Monitor.Enter(monitor);
                    for (int i = 1; i <= numberLimit; i = i + 2)
                    {
                        Console.Write(" " + i);
                        Monitor.Pulse(monitor);


                        bool isLast = i == numberCrash;
                        if (!isLast)
                            Monitor.Wait(monitor);
                        else
                            break;
                    }
                }
                finally
                {
                    Monitor.Exit(monitor);
                }
            }
            public static void Even()
            {
                try
                {
                    Monitor.Enter(monitor);
                    for (int i = 0; i <= numberLimit; i = i + 2)
                    {
                        Console.Write(" " + i);
                        Monitor.Pulse(monitor);

                        bool isLast = i == numberCrash-1;
                        if (!isLast)
                            Monitor.Wait(monitor);
                        else
                            break;
                    }   
                }
                finally
                {
                    Monitor.Exit(monitor);
                }
            }
        }
    }

}
