/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code
            Continuation();

            Console.ReadLine();
        }

        static async Task Continuation()
        {
            Task parentTaskA = Task.Run(() => Console.WriteLine("Parent Task A"))
                .ContinueWith((prevTask) => Console.WriteLine("Continuation Task A"));

            Task parentTaskB = Task.Run(() => throw new Exception("Parent Task B"))
                .ContinueWith((prevTask) => Console.WriteLine("Continuation Task B"), TaskContinuationOptions.OnlyOnFaulted);

            Task parentTaskC = Task.Run(() => throw new Exception("Parent Task C"))
                .ContinueWith((prevTask) => Console.WriteLine("Continuation Task C"), TaskContinuationOptions.ExecuteSynchronously);

            var cts = new CancellationTokenSource();
            Task parentTaskD = Task.Run(() =>
            {
                cts.Cancel();
            });

            Task continuationTaskD = parentTaskD.ContinueWith((prevTask) => Console.WriteLine("Continuation Task D"), TaskContinuationOptions.LongRunning);

            try
            {
                await Task.WhenAll(parentTaskA, parentTaskB, parentTaskC, parentTaskD);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
