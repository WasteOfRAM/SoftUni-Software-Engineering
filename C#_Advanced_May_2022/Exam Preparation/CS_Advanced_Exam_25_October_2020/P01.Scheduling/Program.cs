using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.Scheduling
{
    internal class Program
    {
        static void Main()
        {
            var tasks = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            var threads = new Queue<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            int taskToKill = int.Parse(Console.ReadLine());

            while (true)
            {
                if (tasks.Peek() != taskToKill)
                {
                    if (threads.Peek() >= tasks.Peek())
                    {
                        threads.Dequeue();
                        tasks.Pop();
                    }
                    else
                    {
                        threads.Dequeue();
                    }
                }
                else
                {
                    Console.WriteLine($"Thread with value {threads.Peek()} killed task {taskToKill}");
                    Console.WriteLine(string.Join(" ", threads));
                    break;
                }
            }
        }
    }
}