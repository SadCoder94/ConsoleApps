using System;
using System.Collections.Generic;

namespace PracticeCode
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = {{19, 19, 23, 15, 6},
                        {6, 2, 28, 2, 12},
                         {26, 3, 28, 7, 22},
                         {25, 3, 4, 23, 23},
                         {22, 27, 9, 6, 18}};

            Console.WriteLine(new Program().findMinOpeartion2(matrix, 5));
            printMatrix(matrix, 5);

            //int[,] matrix = {{1, 2, 3},
            //          {4, 2, 3},
            //          {3, 2, 1}};

            //Console.WriteLine(new Program().findMinOpeartion(matrix, 3));
            //printMatrix(matrix, 3);
        }
        static void printMatrix(int[,] matrix,
                                    int n)
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                    Console.Write(matrix[i, j] +
                                            " ");

                Console.WriteLine();
            }
        }

        public int findMinOpeartion(int[,] matrix, int n)
		{

			var sumRow = new int[n];
			var sumCol = new int[n];

			for (int i = 0; i < n; i++)
				for (int j = 0; j < n; j++)
				{
					sumRow[i] += matrix[i,j];
					sumCol[j] += matrix[i,j];
				}

			int maxSum = 0;
			for (int i = 0; i < n; i++)
			{
				maxSum = Math.Max(sumRow[i], maxSum);
				maxSum = Math.Max(sumCol[i], maxSum);
			}

			int count = 0;
			for (int i = 0, j = 0; i < n && j < n;)
			{
				var diff = Math.Min(maxSum - sumRow[i],
							maxSum - sumCol[j]);

				matrix[i,j] += diff;
				sumRow[i] += diff;
				sumCol[j] += diff;

				count += diff;

				if (sumRow[i] == maxSum)
					i++;

				if (sumCol[j] == maxSum)
					j++;
			}
			return count;
		}

        public int findMinOpeartion2(int[,] matrix, int n)
        {
            //code here
            var sumRow = new int[n];
            var sumCol = new int[n];

            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                {
                    sumRow[i] += matrix[i,j];
                    sumCol[i] += matrix[i,j];//Mistake here
                }

            var maxVal = 0;
            for (int i = 0; i < n; ++i)
            {
                maxVal = Math.Max(sumRow[i], maxVal);
                maxVal = Math.Max(sumCol[i], maxVal);
            }

            var minCount = 0;
            for (int i = 0, j = 0; i < n && j < n;)
            {
                var diff = Math.Min(maxVal - sumRow[i], maxVal - sumCol[j]);

                matrix[i,j] += diff;
                sumRow[i] += diff;
                sumCol[j] += diff;

                minCount += diff;

                if (maxVal == sumCol[j]) ++j;
                if (maxVal == sumRow[i]) ++i;
            }

            return minCount;
        }
    }
}
