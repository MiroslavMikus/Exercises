using System;

namespace SomeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public static class Calculator
    {
        public static int Add(int a, int b) => a + b;
    }
}