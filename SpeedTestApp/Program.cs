using System;
using System.Collections.Generic;
using System.Diagnostics;
using DeepCopyExtensions;
using DeepCopyTestClasses;

namespace SpeedTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //////////////// SPEED TEST //////////////////

            var watches0 = Stopwatch.StartNew();

            var list = new List<ModerateClass>();

            for (int i = 0; i < 10000; i++)
            {
                list.Add(ModerateClass.CreateForTests(i));
            }

            Console.WriteLine("List of " + list.Count + " objects generated in: " + watches0.ElapsedMilliseconds);

            var watches1 = Stopwatch.StartNew();

            list.ForEach(a => DeepCopyBySerialization.DeepClone(a));

            Console.WriteLine("By Serialization: " + watches1.ElapsedMilliseconds);

            var watches2 = Stopwatch.StartNew();

            list.ForEach(a => DeepCopyByReflection.Copy(a));

            Console.WriteLine("By Reflection: " + watches2.ElapsedMilliseconds);

            var watches3 = Stopwatch.StartNew();

            list.ForEach(a => DeepCopyByExpressionTrees.DeepCopyByExpressionTree(a));

            Console.WriteLine("By Expression Trees: " + watches3.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
