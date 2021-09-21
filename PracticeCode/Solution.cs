using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

static class Result
{

    /*
     * Complete the 'almostSorted' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static void almostSorted(List<int> arr)
    {
        List<int> sorted = arr.Select(x => x).ToList(); sorted.Sort();

        if (arr.SequenceEqual(sorted))
        {
            Console.Write("yes");
            return;
        }

        int l = -1, r = -1;

        var n = arr.Count();

        for (var i = 0; i < n - 1; i++)
            if (arr[i] > arr[i + 1])
            {
                l = i;
                break;
            }

        for (var i = n - 1; i >= 0; i--)
            if (arr[i] < arr[i - 1])
            {
                r = i;
                break;
            }

        //try swap
        List<int> deepcpy = arr.Select(x => x).ToList();
        var swapTry = deepcpy.Swap(l, r);
        if (sorted.SequenceEqual(swapTry))
        {
            Console.WriteLine("yes");
            Console.Write("swap " + l.ToString() + " " + r.ToString());
            return;
        }

        //try reverse
        deepcpy = arr.Select(x => x).ToList();
        var revTry = deepcpy;
        var revTrySub = revTry.GetRange(l, r - l + 1);//extract the sub seq
        revTry.RemoveRange(l, r - l + 1);//remove sub seq
        revTrySub.Sort();//sort sub seq
        revTry.InsertRange(l, revTrySub);//insert sorted sub seq to original position 

        if (sorted.SequenceEqual(revTry))
        {
            Console.WriteLine("yes");
            Console.Write("reverse " + l.ToString() + " " + r.ToString());
            return;
        }

        Console.WriteLine("no");

    }

    public static List<int> Swap(this List<int> arr, int i, int j)
    {
        var temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
        return arr;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        //int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = new List<int> { 1,5,4,3,2,6};

        Result.almostSorted(arr);
    }
}
