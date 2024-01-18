using System;
using System.Collections.Generic;


public static class UtilMath
{
    public static int GetFibonacciRecursively(int n)
    {
        if (n < 0)
        {
            throw new ArgumentException("Input must be a non-negative integer.");
        }
        
        //This works because C# starts everything at 0, and the only Fibonacci = 0 is in the 0 position, which is unreachable
        int[] memo = new int[n];

        return FibonacciRecursion(memo, n);
    }

    private static int FibonacciRecursion(int[] fibonacciMemo, int n)
    {
        if (n < 2) return n; //0, 1, 1

        if (fibonacciMemo[n-1] != 0) return fibonacciMemo[n-1];

        return fibonacciMemo[n-1] = FibonacciRecursion(fibonacciMemo, n - 1) + FibonacciRecursion(fibonacciMemo, n - 2);
    }
}
