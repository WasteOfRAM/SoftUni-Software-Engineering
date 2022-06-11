using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassroomProject
{
    internal class Classroom
    {
        private List<Student> students;

        public Classroom(int capacity)
        {
            this.Capacity = capacity;
            this.students = new List<Student>();
        }

        public int Capacity { get; private set; }
        public int Count { get => this.students.Count; }

        public string RegisterStudent(Student student)
        {
            if(students.Count < this.Capacity)
            {
                this.students.Add(student);
                return $"Added student {student.FirstName} {student.LastName}";
            }
            else
            {
                return "No seats in the classroom";
            }
        }

        public string DismissStudent(string firstName, string lastName)
        {
            Student studentToRemove = students.Find(s => s.FirstName == firstName && s.LastName == lastName);

            if(students.Remove(studentToRemove))
            {
                return $"Dismissed student {firstName} {lastName}";
            }
            else
            {
                return "Student not found";
            }
        }

        public string GetSubjectInfo(string subject)
        {
            var studentsBySubject = students.Where(s => s.Subject == subject).ToList();

            if (studentsBySubject.Count == 0)
                return "No students enrolled for the subject";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Subject: {subject}");
            sb.AppendLine("Students:");

            foreach (var student in studentsBySubject)
            {
                sb.AppendLine($"{student.FirstName} {student.LastName}");
            }
            sb.Remove(sb.Length - 1, 1);


            return sb.ToString();
        }

        public int GetStudentsCount()
        {
            return students.Count;
        }

        public Student GetStudent(string firstName, string lastName)
        {
            return students.Find(s => s.FirstName == firstName && s.LastName == lastName);
        }
    }
}
