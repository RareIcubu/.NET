using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTask;

internal class Program
{
    static void Main(string[] args)
    {
        int n = 700;
        int t = 2;
        int iterations = 5;

        if (args.Length > 0 && int.TryParse(args[0], out int parsedN)) n = parsedN;
        if (args.Length > 1 && int.TryParse(args[1], out int parsedT)) t = parsedT;
        if (args.Length > 2 && int.TryParse(args[2], out int parsedI)) iterations = parsedI;

        Console.WriteLine($"Rozmiar macierzy: {n}x{n}, Liczba wątków: {t}, Ilość prób: {iterations}");

        int[,] matrix1 = GenerateMatrix(n);
        int[,] matrix2 = GenerateMatrix(n);

        long totalSeq = 0, totalPar = 0, totalThread = 0;
        int[,] resultSeq = new int[n,n];
        int[,] resultPar = new int[n,n];
        int[,] resultThread = new int[n,n];

        for (int i = 0; i < iterations; i++)
        {
            var sw = Stopwatch.StartNew();
            resultSeq = MultiplySequential(matrix1, matrix2, n);
            sw.Stop();
            totalSeq += sw.ElapsedMilliseconds;

            sw.Restart();
            resultPar = MultiplyParallel(matrix1, matrix2, n, t);
            sw.Stop();
            totalPar += sw.ElapsedMilliseconds;

            sw.Restart();
            resultThread = MultiplyThread(matrix1, matrix2, n, t);
            sw.Stop();
            totalThread += sw.ElapsedMilliseconds;
        }

        Console.WriteLine($"Średni czas sekwencyjny: {totalSeq / iterations} ms");
        Console.WriteLine($"Średni czas Parallel.For: {totalPar / iterations} ms");
        Console.WriteLine($"Średni czas Thread: {totalThread / iterations} ms");

        if (n <= 10)
        {
            Console.WriteLine("Zgodność wyników: " + CheckMatrices(resultSeq, resultPar, resultThread, n));
        }
    }

    static int[,] GenerateMatrix(int n)
    {
        Random rand = new Random();
        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matrix[i, j] = rand.Next(1, 10);
        return matrix;
    }

    static int[,] MultiplySequential(int[,] a, int[,] b, int n)
    {
        int[,] c = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int sum = 0;
                for (int k = 0; k < n; k++) sum += a[i, k] * b[k, j];
                c[i, j] = sum;
            }
        }
        return c;
    }

    static int[,] MultiplyParallel(int[,] a, int[,] b, int n, int threads)
    {
        int[,] c = new int[n, n];
        ParallelOptions opt = new ParallelOptions { MaxDegreeOfParallelism = threads };
        System.Threading.Tasks.Parallel.For(0, n, opt, i =>
        {
            for (int j = 0; j < n; j++)
            {
                int sum = 0;
                for (int k = 0; k < n; k++) sum += a[i, k] * b[k, j];
                c[i, j] = sum;
            }
        });
        return c;
    }

    static int[,] MultiplyThread(int[,] a, int[,] b, int n, int threads)
    {
        int[,] c = new int[n, n];
        Thread[] threadArray = new Thread[threads];
        int rowsPerThread = n / threads;

        for (int t = 0; t < threads; t++)
        {
            int startRow = t * rowsPerThread;
            int endRow = (t == threads - 1) ? n : startRow + rowsPerThread;

            threadArray[t] = new Thread(() =>
            {
                for (int i = startRow; i < endRow; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < n; k++) sum += a[i, k] * b[k, j];
                        c[i, j] = sum;
                    }
                }
            });
            threadArray[t].Start();
        }

        foreach (var thread in threadArray)
        {
            thread.Join();
        }

        return c;
    }

    static bool CheckMatrices(int[,] m1, int[,] m2, int[,] m3, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (m1[i, j] != m2[i, j] || m1[i, j] != m3[i, j]) return false;
            }
        }
        return true;
    }
}