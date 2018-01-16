using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest1
{
    class Buffer
    {

        private Queue<int> queue = new Queue<int>();

        private static object objToLock = new object();

        public void Put(int data)
        {
            Monitor.Enter(objToLock);

            queue.Enqueue(data);
            Monitor.Pulse(objToLock);
            Monitor.Exit(objToLock);
        }

        public int Get()
        {
            Monitor.Enter(objToLock);

            Console.WriteLine("Queue count: "+queue.Count);
            while(queue.Count==0)
            {
                Monitor.Wait(objToLock);
            }
            int data = queue.Dequeue();

            Monitor.Pulse(objToLock);
            Monitor.Exit(objToLock);

            return data;
        }

    }
}
