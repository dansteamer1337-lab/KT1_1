using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp4
{
    internal class Program
    {
        static int[] generateArray(int n)
        {
            int[] array = new int[n];
            Stopwatch sw = Stopwatch.StartNew();

            Parallel.For(0, n, i =>
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                array[i] = rnd.Next(-4500, 4500);
            });

            sw.Stop();
            Console.WriteLine("===========================================");
            Console.WriteLine($"Массив из {n} элементов, Время генерации: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine("===========================================");

            return array;
        }

        static long summary(int[] array, int threadCount)
        {
            Stopwatch sw = Stopwatch.StartNew();
            long totalSum = 0;

            int chunkSize = array.Length / threadCount;
            long[] partialSums = new long[threadCount];

            Parallel.For(0, threadCount, i =>
            {
                long localSum = 0;
                int start = i * chunkSize;
                int end = (i == threadCount - 1) ? array.Length : start + chunkSize;

                Console.WriteLine($"[{DateTime.Now:mm:ss.fff}] Поток {Thread.CurrentThread.ManagedThreadId} " +
                                 $"обрабатывает чанк {i}: элементы {start}-{end - 1}");

                for (int j = start; j < end; j++)
                {
                    localSum += array[j];
                }

                partialSums[i] = localSum;
            });

            for (int i = 0; i < threadCount; i++)
            {
                totalSum += partialSums[i];
            }

            sw.Stop();
            Console.WriteLine("===========================================");
            Console.WriteLine($"Сумма: {totalSum}, Время подсчета: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine("===========================================");

            return totalSum;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите размер массива: ");
            int n = Convert.ToInt32(Console.ReadLine());

            int[] array = generateArray(n);

            Console.WriteLine("\nЗапускаем параллельный подсчет:");
            long parallelSum = summary(array, 3);

            Console.WriteLine($"Параллель: {parallelSum}");

            Console.ReadLine();
        }
    }
}