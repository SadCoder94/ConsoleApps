using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class GfG
{
    public class Pair: IComparable<Pair>
    {
        public int li { get; set; } 
        public int di { get; set; } 
        public int val { get; set; }

        public Pair(int li, int di, int val)
        {
            this.li = li;
            this.val = val;
            this.di = di;
        }

        public int CompareTo(Pair other)
        {
            return this.val - other.val;
        }
    }
    public class Solution
    {
        public static void Main(string[] args)
        {
            // you can write to stdout for debugging purposes, e.g.

            var arr = new int[][] { new int[] { 1, 3, 7, 9, 20 }, new int[] { 14, 53, 56, 75, 89 }, new int[] { 16, 59, 89, 800, 950 } };

            var list = Sort(arr);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        private static List<int> Sort(int[][] arr)
        {
            PriorityQueue<Pair, int> soLi = new PriorityQueue<Pair, int>();
            List<int> sorFin = new List<int>();

            for (var i=0; i < arr.Length; i++)
            {
                Pair pair = new Pair(i, 0, arr[i][0]);
                soLi.Enqueue(pair, pair.val);
            }

            while( soLi.Count > 0)
            {
                Pair p = soLi.Dequeue();
                sorFin.Add(p.val);
                p.di++;

                if (p.di < arr[p.li].Length)
                {
                    p.val = arr[p.li][p.di];
                    soLi.Enqueue(p, p.val);
                }
            }

            return sorFin;
        }
    }
}
