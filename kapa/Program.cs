using System;
using System.Collections.Concurrent;

namespace kapa
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<string, string> dic = null;

            foreach (var pod in dic)
            {
                Console.WriteLine();
            }
        }
    }
}