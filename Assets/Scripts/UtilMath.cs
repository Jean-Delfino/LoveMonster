using System;
using System.Collections;
using System.Collections.Generic;


public static class UtilMath
{
    //This is the only way without using loops
    //The other way would be using a int[] as memo, but we would need to set all vector values as -1

    public static int GetFibonacciRecursively(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException("Input must be a non-negative integer.");
        }
        
        Dictionary<int, int> memo = new();

        return FibonacciRecursion(memo, n);
    }

    private static int FibonacciRecursion(Dictionary<int, int> fibonacciMemo, int n)
    {
        if (n < 2) return n; //0, 1, 1

        if (fibonacciMemo.ContainsKey(n)) return fibonacciMemo[n];

        return fibonacciMemo[n] = FibonacciRecursion(fibonacciMemo, n - 1) + FibonacciRecursion(fibonacciMemo, n - 2);
    }
}
