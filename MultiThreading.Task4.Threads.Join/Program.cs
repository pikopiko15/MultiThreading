/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static Semaphore semaphore = new Semaphore(10, 100);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code
            int initialNumber = 10;

            Thread thread = new Thread(DecrementAndPrintWithJoin);
            thread.Start(initialNumber);
            thread.Join();

            ThreadPool.QueueUserWorkItem(DecrementAndPrintWithSemaphore, initialNumber);

            Console.ReadLine();
        }

        static void DecrementAndPrintWithJoin(object state)
        {
            int number = (int)state;
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {number}");
            number--;

            if (number > 0)
            {
                Thread newThread = new Thread(DecrementAndPrintWithJoin);
                newThread.Start(number);
                newThread.Join();
            }
        }

        static void DecrementAndPrintWithSemaphore(object state)
        {
            int number = (int)state;
            semaphore.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: {number}");
            number--;

            if (number > 0)
            {
                ThreadPool.QueueUserWorkItem(DecrementAndPrintWithSemaphore, number);
            }

            semaphore.Release();
        }
    }
}
