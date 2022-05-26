using System;
using System.Collections.Generic;
using System.Linq;

namespace P09.SoftUni_Exam_Results
{
    internal class Program
    {
        static void Main()
        {
            var students = new Dictionary<string, int>();
            var contestsByLanguage = new Dictionary<string, int>();

            string input;
            while ((input = Console.ReadLine()) != "exam finished")
            {
                string[] inputSplit = input.Split('-', StringSplitOptions.RemoveEmptyEntries);

                if (inputSplit.Length == 3)
                {
                    string studentName = inputSplit[0];
                    string contestName = inputSplit[1];
                    int contestScore = int.Parse(inputSplit[2]);

                    // Fill the students with scores
                    if (!students.ContainsKey(studentName))
                    {
                        students[studentName] = contestScore;
                    }
                    else
                    {
                        if (students[studentName] < contestScore)
                        {
                            students[studentName] = contestScore;
                        }
                    }


                    // Contests by language count
                    if (!contestsByLanguage.ContainsKey(contestName))
                    {
                        contestsByLanguage[contestName] = 1;
                    }
                    else
                    {
                        contestsByLanguage[contestName]++;
                    }
                }
                else
                {
                    string cheater = inputSplit[0];

                    students.Remove(cheater);
                }
            }

            students = students.OrderByDescending(s => s.Value).ThenBy(s => s.Key).ToDictionary(k => k.Key, v => v.Value);
            contestsByLanguage = contestsByLanguage.OrderByDescending(c => c.Value).ThenBy(c => c.Key).ToDictionary(k => k.Key, v => v.Value);

            Console.WriteLine("Results:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Key} | {student.Value}");
            }

            Console.WriteLine("Submissions:");
            foreach (var contest in contestsByLanguage)
            {
                Console.WriteLine($"{contest.Key} - {contest.Value}");
            }
        }
    }
}
