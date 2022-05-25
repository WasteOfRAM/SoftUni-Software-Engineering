using System;
using System.Collections.Generic;
using System.Linq;

namespace P08.Ranking
{
    internal class Program
    {
        static void Main()
        {
            var contests = new Dictionary<string, string>();
            var students = new SortedDictionary<string, Dictionary<string, int>>();

            string input;
            while ((input = Console.ReadLine()) != "end of contests")
            {
                contests.Add(input.Split(':')[0], input.Split(':')[1]);
            }

            while ((input = Console.ReadLine()) != "end of submissions")
            {
                string studentName = input.Split("=>")[2];
                string contest = input.Split("=>")[0];
                int points = int.Parse(input.Split("=>")[3]);
                string password = input.Split("=>")[1];

                if (contests.ContainsKey(contest) && contests[contest] == password)
                {
                    if (!students.ContainsKey(studentName))
                        students[studentName] = new Dictionary<string, int>() { { contest, points } };
                    else
                        if(!students[studentName].ContainsKey(contest))
                            students[studentName].Add(contest, points);
                        else
                            if(students[studentName][contest] < points)
                                students[studentName][contest] = points;
                }

            }


            var bestStudent = students.OrderByDescending(s => s.Value.Sum(i => i.Value)).First();
            

            Console.WriteLine($"Best candidate is { bestStudent.Key } with total {bestStudent.Value.Sum(i => i.Value)} points.");
            Console.WriteLine("Ranking: ");
            foreach (var student in students)
            {
                Console.WriteLine(student.Key);
                foreach (var (contestName, points) in student.Value.OrderByDescending(p => p.Value))
                {
                    Console.WriteLine($"#  {contestName} -> {points}");
                }
            }
        }
    }
}
