/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code
            RunChaining();

            Console.ReadLine();
        }

        static async Task RunChaining()
        {
            await Task.Run(() => CreateRandomArray(10)).ContinueWith(x => MultiplyArray(x.Result))
                                                       .ContinueWith(x => SortAscending(x.Result))
                                                       .ContinueWith(x => CalculateAverage(x.Result));
        }

        static int[] CreateRandomArray(int size)
        {
            var rnd = new Random();
            var array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = rnd.Next(7, 77);
            }

            Console.WriteLine($"Initial array is [{string.Join(", ", array)}].");
            return array;
        }

        static int[] MultiplyArray(int[] array)
        {
            var rnd = new Random();
            var val = rnd.Next(2, 10);

            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i] * val;
            }

            Console.WriteLine($"This array multiplied by {val} is [{string.Join(", ", result)}].");
            return result;
        }

        static int[] SortAscending(int[] array)
        {
            Array.Sort(array);

            Console.WriteLine($"This array sorted by ascending is [{string.Join(", ", array)}].");
            return array;
        }

        static void CalculateAverage(int[] array)
        {
            Console.WriteLine($"The average of the final array is {array.Average()}");
        }
    }
}
