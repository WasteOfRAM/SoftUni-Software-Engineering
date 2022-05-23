using System;
using System.Collections.Generic;
using System.Linq;

namespace P02.Average_Student_Grades
{
    internal class Program
    {
        static void Main()
        {
            int studentsCount = int.Parse(Console.ReadLine());

            var studentGrades = new Dictionary<string, List<decimal>>();

            for (int i = 0; i < studentsCount; i++)
            {
                string[] curentStudent = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if(!studentGrades.ContainsKey(curentStudent[0]))
                    studentGrades[curentStudent[0]] = new List<decimal>();

                studentGrades[curentStudent[0]].Add(decimal.Parse(curentStudent[1]));
            }

            foreach (var (name, grades) in studentGrades)
            {
                Console.WriteLine($"{name} -> {string.Join(' ',grades.Select(grade => grade.ToString("f2")))} (avg: {grades.Average():f2})");
            }
        }
    }
}
