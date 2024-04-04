using System;
using System.Threading;

class Program
{
    static int[,] matrix = new int[,]
    {
        {1, 2, 3, 4},
        {5, 6, 7, 8},
        {9, 10, 11, 12},
        {13, 14, 15, 16}
    };

    static int sum = 0;
    static int numThreads = 4; 

    static void Main(string[] args)
    {
        Thread[] threads = new Thread[numThreads];

      
        for (int i = 0; i < numThreads; i++)
        {
            threads[i] = new Thread(AddPartialSum);
            threads[i].Start(i);
        }

        
        foreach (Thread t in threads)
        {
            t.Join();
        }
        
        Console.WriteLine("Suma total: " + sum);
    }

    static void AddPartialSum(object threadIndex)
    {
        int index = (int)threadIndex;
        int elementsPerThread = matrix.Length / numThreads;
        int startIndex = index * elementsPerThread;
        int endIndex = (index == numThreads - 1) ? matrix.Length : (index + 1) * elementsPerThread;

        int partialSum = 0;

      
        for (int i = startIndex; i < endIndex; i++)
        {
            partialSum += matrix[i / matrix.GetLength(1), i % matrix.GetLength(1)];
        }

        
        Interlocked.Add(ref sum, partialSum);
    }
}
