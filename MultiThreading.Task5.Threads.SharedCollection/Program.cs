/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            // feel free to add your code
            List<int> sharedCollection = new List<int>();

            Semaphore writeConcurrentOperation = new Semaphore(0, 1);
            writeConcurrentOperation.Release();

            Semaphore readConcurrentOperation = new Semaphore(0, 1);

            Thread writeToCollection = new Thread(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    writeConcurrentOperation.WaitOne();
                    sharedCollection.Add(i);
                    readConcurrentOperation.Release();
                }
            });

            Thread readFromCollection = new Thread((arg) =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    readConcurrentOperation.WaitOne();
                    Console.WriteLine("[" + string.Join(", ", sharedCollection) + "]");
                    writeConcurrentOperation.Release();
                }
            });

            writeToCollection.Start();
            readFromCollection.Start();

            Console.ReadLine();
        }
    }
}
