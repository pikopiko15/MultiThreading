/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();

            HundredTasksAsync();

            Console.ReadLine();
        }

        static async Task HundredTasksAsync()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < TaskAmount; i++)
            {
                tasks.Add(Output(i, MaxIterationsCount));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("All tasks have completed.");
        }

        static Task Output(int taskNumber, int iterationNumber)
        {
            for (int i = 1; i <= iterationNumber; i++)
            {
                Console.WriteLine($"Task #{taskNumber} – {i}");
                
            }
            return Task.CompletedTask;
        }
    }
}
