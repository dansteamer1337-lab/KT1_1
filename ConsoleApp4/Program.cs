using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ConsoleApp4
{
    internal class Program
    {

        static int[] generateArray(int n) 
        {

            int[] array = new int[n];
            Random rnd = new Random();

            Parallel.For(0, n, i => {
                array[i] = rnd.Next(-4500, 4500);
            });

            return array;
        }

        static long summary(int[] array, int count)
        {
            int chunkSize = array.Length / count;
            int[] partialSums = new int[count];
            long totalSum = 0;


            Parallel.For(0, count, i =>
            {
                int localSum = 0;
                int start = i * chunkSize;
                int end = (i == count-1) ? array.Length : start + chunkSize;

                for (int j = start; j < end; j++)
                {
                    localSum = array[j];
                }
                partialSums[i] = localSum;
                 
            });

            for (int i = 0; i < count; i++)
            {
                totalSum = partialSums[i];
            }


            return totalSum;
        }

        static void Main(string[] args)
        {

            int n = Convert.ToInt32(Console.ReadLine());
            int[] array = new int[n];
            array = generateArray(n);

            Console.WriteLine(summary(array, n));

            Console.ReadLine();
        }


    }
}
