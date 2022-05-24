using System;
using System.Collections.Generic;

namespace RandomCode
{
    public class Program
    {
        static void Main(string[] args)
        {
           Solution solution = new Solution();
            int[][] matrix = new[] { new[] { 0, 0, 0 },new[] { 0, 1, 0 },new[] { 0, 0, 0 } };

             solution.UniquePathsWithObstacles(matrix);
        }
    }

    public class Solution
    {
        private (int, int)[] indexes = { (0, 1), (-1, 0) };
        private int n, m;
        private int count = 0;

        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {

            m = obstacleGrid.Length;
            n = obstacleGrid[0].Length;

            DFS(obstacleGrid, 0, 0);
            return count;
        }

        private void DFS(int[][] grid, int i, int j)
        {
            if (i == m - 1 && j == n - 1)
                count++;

            foreach (var (pi, pj) in indexes)
            {
                int ni = i + pi, nj = j + pj;
                if (ni >= 0 && ni < m && nj >= 0 && nj < n && grid[ni][nj] != 1)
                {
                    Console.WriteLine(ni);
                    DFS(grid, ni, nj);
                }
            }

        }
    }
}
